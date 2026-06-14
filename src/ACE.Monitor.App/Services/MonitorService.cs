using ACE.Monitor.App.Models;

namespace ACE.Monitor.App.Services;

public sealed class MonitorService
{
    private readonly ProcessLimiter _limiter;
    private readonly Logger _logger;
    private CancellationTokenSource? _cts;

    public bool IsRunning => _cts is { IsCancellationRequested: false };
    public int LastFoundCount { get; private set; }
    public DateTime? LastScanTime { get; private set; }

    public event Action<IReadOnlyList<ProcessLimitResult>>? ScanCompleted;

    public MonitorService(ProcessLimiter limiter, Logger logger)
    {
        _limiter = limiter;
        _logger = logger;
    }

    public void Start(MonitorSettings settings)
    {
        if (IsRunning) return;

        _cts = new CancellationTokenSource();
        _logger.Info($"ACE Monitor App 启动。初始间隔={settings.IntervalSeconds}s，CPU单核限制={settings.EnableCpuLimit}，优先级限制={settings.EnablePriorityLimit}");

        _ = Task.Run(async () =>
        {
            int dynamicInterval = settings.IntervalSeconds;
            int consecutiveMisses = 0;

            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    var results = _limiter.Apply(settings);
                    LastFoundCount = results.Count;
                    LastScanTime = DateTime.Now;
                    ScanCompleted?.Invoke(results);

                    if (results.Count == 0)
                    {
                        // 未发现进程 → 逐渐增加间隔
                        consecutiveMisses++;
                        int extraWait = (consecutiveMisses / 3) * 10;
                        dynamicInterval = Math.Min(120, settings.IntervalSeconds + extraWait);
                        _logger.Info($"本轮未发现目标 ACE 进程。下次扫描间隔调整为 {dynamicInterval} 秒。");
                    }
                    else
                    {
                        // 发现进程 → 重置未命中计数，缩短间隔进行严盯
                        consecutiveMisses = 0;
                        dynamicInterval = Math.Max(5, settings.IntervalSeconds / 2);
                        _logger.Info($"本次发现 {results.Count} 个目标进程，下次扫描间隔缩短至 {dynamicInterval} 秒。");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"监控循环异常：{ex.Message}");
                }

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(dynamicInterval), _cts.Token);
                }
                catch (TaskCanceledException) { }
            }
        }, _cts.Token);
    }

    public void Stop()
    {
        if (!IsRunning) return;
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
        _logger.Info("ACE Monitor App 已停止");
    }
}