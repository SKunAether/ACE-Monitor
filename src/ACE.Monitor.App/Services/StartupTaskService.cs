using System.Diagnostics;
using System.IO;

namespace ACE.Monitor.App.Services;

public sealed class StartupTaskService
{
    private const string TaskName = "ACE-Monitor-App";
    private readonly Logger _logger;

    public StartupTaskService(Logger logger)
    {
        _logger = logger;
    }

    public bool IsEnabled()
    {
        using var process = Process.Start(new ProcessStartInfo
        {
            FileName = "schtasks.exe",
            Arguments = $"/Query /TN \"{TaskName}\"",
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        });
        process?.WaitForExit(3000);
        return process?.ExitCode == 0;
    }

    public bool Enable()
    {
        var exe = Environment.ProcessPath ?? Path.Combine(AppContext.BaseDirectory, "ACE-Monitor.exe");
        // 直接启动 exe，无需 cmd 过渡，避免弹出黑色窗口
        var args = $"/Create /F /TN \"{TaskName}\" /SC ONLOGON /RL HIGHEST /TR \"\\\"{exe}\\\" --minimized --autostart\"";
        return RunSchtasks(args, "已添加开机自启任务（最高权限）");
    }

    public bool Disable() => RunSchtasks($"/Delete /F /TN \"{TaskName}\"", "已移除开机自启任务");

    private bool RunSchtasks(string arguments, string successMessage)
    {
        try
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = "schtasks.exe",
                Arguments = arguments,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });
            process?.WaitForExit(5000);
            if (process?.ExitCode == 0)
            {
                _logger.Info(successMessage);
                return true;
            }

            var error = process?.StandardError.ReadToEnd();
            _logger.Error($"开机自启操作失败：{error}");
        }
        catch (Exception ex)
        {
            _logger.Error($"开机自启操作异常：{ex.Message}");
        }
        return false;
    }
}