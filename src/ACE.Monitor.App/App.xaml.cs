using System.Windows;

namespace ACE.Monitor.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // 强制将当前工作目录设为程序所在目录，避免计划任务启动时工作目录为 System32
        Environment.CurrentDirectory = AppContext.BaseDirectory;

        // 解析命令行参数
        var args = Environment.GetCommandLineArgs();
        bool minimized = args.Any(a => a.Equals("--minimized", StringComparison.OrdinalIgnoreCase));
        bool autoStart = args.Any(a => a.Equals("--autostart", StringComparison.OrdinalIgnoreCase));

        var mainWindow = new MainWindow(autoStart);
        if (minimized)
        {
            mainWindow.WindowState = WindowState.Minimized;
            mainWindow.ShowInTaskbar = true;
        }

        mainWindow.Show();
    }
}