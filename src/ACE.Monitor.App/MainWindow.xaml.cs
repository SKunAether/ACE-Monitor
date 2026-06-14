using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using ACE.Monitor.App.Models;
using ACE.Monitor.App.Services;

namespace ACE.Monitor.App;

public partial class MainWindow : Window
{
    private readonly Logger _logger = new();
    private readonly MonitorService _monitor;
    private readonly StartupTaskService _startup;
    private bool _loadingStartupState = true;
    private readonly bool _autoStart;

    public MainWindow(bool autoStart = false)
    {
        InitializeComponent();
        _autoStart = autoStart;
        _monitor = new MonitorService(new ProcessLimiter(_logger), _logger);
        _startup = new StartupTaskService(_logger);

        _logger.MessageLogged += line => Dispatcher.Invoke(() =>
        {
            LogList.Items.Insert(0, line);
            while (LogList.Items.Count > 300) LogList.Items.RemoveAt(LogList.Items.Count - 1);
        });

        _monitor.ScanCompleted += results => Dispatcher.Invoke(() =>
        {
            FoundCountText.Text = results.Count.ToString();
            LastScanText.Text = DateTime.Now.ToString("HH:mm:ss");
        });

        StartupCheck.IsChecked = _startup.IsEnabled();
        _loadingStartupState = false;

        // 窗口加载完成后，若需要自动启动则开始监控（此时控件已就绪）
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (_autoStart)
        {
            StartMonitor();
        }
    }

    private MonitorSettings ReadSettings()
    {
        _ = int.TryParse(IntervalBox.Text, out var seconds);
        return new MonitorSettings
        {
            IntervalSeconds = Math.Clamp(seconds <= 0 ? 30 : seconds, 5, 3600),
            EnablePriorityLimit = PriorityCheck.IsChecked == true,
            EnableCpuLimit = CpuCheck.IsChecked == true,
            CpuAffinityMask = 1
        };
    }

    private void StartButton_Click(object sender, RoutedEventArgs e) => StartMonitor();

    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        _monitor.Stop();
        SetRunningState(false);
    }

    private void StartMonitor()
    {
        _monitor.Start(ReadSettings());
        SetRunningState(true);
    }

    private void SetRunningState(bool running)
    {
        StatusText.Text = running ? "运行中" : "未运行";
        StatusDot.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(running ? "#51E3A4" : "#FFD166"));
        StartButton.IsEnabled = !running;
        StopButton.IsEnabled = running;
    }

    private void StartupCheck_Changed(object sender, RoutedEventArgs e)
    {
        if (_loadingStartupState) return;
        var ok = StartupCheck.IsChecked == true ? _startup.Enable() : _startup.Disable();
        if (!ok)
        {
            MessageBox.Show("开机自启设置失败，请确认以管理员身份运行。", "ACE Monitor", MessageBoxButton.OK, MessageBoxImage.Warning);
            _loadingStartupState = true;
            StartupCheck.IsChecked = _startup.IsEnabled();
            _loadingStartupState = false;
        }
    }

    private void OpenLogsButton_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = _logger.LogDirectory,
            UseShellExecute = true
        });
    }

    protected override void OnClosed(EventArgs e)
    {
        _monitor.Stop();
        base.OnClosed(e);
    }
}