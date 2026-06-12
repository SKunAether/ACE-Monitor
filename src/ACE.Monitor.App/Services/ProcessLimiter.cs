using System.Diagnostics;
using ACE.Monitor.App.Models;

namespace ACE.Monitor.App.Services;

public sealed class ProcessLimiter
{
    private readonly Logger _logger;

    public ProcessLimiter(Logger logger)
    {
        _logger = logger;
    }

    public IReadOnlyList<ProcessLimitResult> Apply(MonitorSettings settings)
    {
        var results = new List<ProcessLimitResult>();
        foreach (var name in settings.TargetProcessNames)
        {
            Process[] processes;
            try
            {
                processes = Process.GetProcessesByName(name);
            }
            catch (Exception ex)
            {
                _logger.Error($"读取进程 {name} 失败：{ex.Message}");
                continue;
            }

            foreach (var process in processes)
            {
                var priorityApplied = false;
                var cpuApplied = false;
                string? error = null;

                try
                {
                    if (settings.EnablePriorityLimit && process.PriorityClass != ProcessPriorityClass.Idle)
                    {
                        process.PriorityClass = ProcessPriorityClass.Idle;
                        priorityApplied = true;
                    }

                    if (settings.EnableCpuLimit)
                    {
                        process.ProcessorAffinity = new IntPtr(settings.CpuAffinityMask);
                        cpuApplied = true;
                    }

                    if (priorityApplied || cpuApplied)
                    {
                        _logger.Info($"已限制 {process.ProcessName}.exe PID:{process.Id} Priority={(settings.EnablePriorityLimit ? "Idle" : "未启用")} CPU核心=0");
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    _logger.Error($"限制 {process.ProcessName}.exe PID:{process.Id} 失败：{ex.Message}");
                }

                results.Add(new ProcessLimitResult(process.ProcessName, process.Id, priorityApplied, cpuApplied, error));
                process.Dispose();
            }
        }

        return results;
    }
}
