using System.IO;
using System.Text;

namespace ACE.Monitor.App.Services;

public sealed class Logger
{
    public string LogDirectory { get; }
    public string CurrentLogFile => Path.Combine(LogDirectory, $"ACE_Monitor_{DateTime.Now:yyyyMMdd}.log");

    public event Action<string>? MessageLogged;

    public Logger()
    {
        LogDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
        Directory.CreateDirectory(LogDirectory);
    }

    public void Info(string message) => Write("INFO", message);
    public void Error(string message) => Write("ERROR", message);

    private void Write(string level, string message)
    {
        var line = $"[{DateTime.Now:HH:mm:ss}] [{level}] {message}";
        File.AppendAllText(CurrentLogFile, line + Environment.NewLine, Encoding.UTF8);
        MessageLogged?.Invoke(line);
    }
}
