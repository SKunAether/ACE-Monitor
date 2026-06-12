<div align="center">

# 🛡️ ACE Monitor App

Windows desktop tool for monitoring and limiting Tencent ACE processes

</div>

---

## Introduction

ACE Monitor App is a desktop application rewrite of the original PowerShell script. It continuously monitors three specific Tencent ACE‑related processes and automatically applies resource limits once they are detected:

- Sets the target process priority to `Idle`
- Restricts CPU affinity to a single core (Core 0)
- Logs all scan and limit operations
- Allows enabling/disabling monitoring via the UI
- Supports adding to Windows startup, starting minimized and automatically monitoring

> ⚠️ This program requires administrator privileges to modify process priority or CPU affinity.

---

## Key Features in This Version

This WPF desktop version focuses on: monitoring exactly three processes, startup integration, minimized launch, and one‑click publishing.

| Feature | Implementation |
|---------|----------------|
| Start / Stop | Click “Start Monitoring / Stop” in the UI |
| Priority limit | Enabled by default, sets priority to Idle |
| Single‑core limit | Enabled by default, restricts to CPU core 0 |
| Startup integration | UI checkbox creates a Windows Task Scheduler task that launches with `--minimized --autostart` on user logon |
| Logging | `ProgramDirectory\Logs\ACE_Monitor_YYYYMMDD.log` |
| Permissions | `app.manifest` requires administrator rights |

---

## Monitored Processes

- `SGuard64.exe`
- `SGuardSvc64.exe`
- `ACE-GuardClient.exe`

---

## Project Structure

```text
ACE-Monitor-App/
├── ACE-Monitor-App.sln
├── build-release.bat
├── README.md
├── LICENSE
├── .gitignore
└── src/
    └── ACE.Monitor.App/
        ├── ACE.Monitor.App.csproj
        ├── app.manifest
        ├── App.xaml
        ├── MainWindow.xaml
        ├── MainWindow.xaml.cs
        ├── Models/
        │   ├── MonitorSettings.cs
        │   └── ProcessLimitResult.cs
        └── Services/
            ├── Logger.cs
            ├── MonitorService.cs
            ├── ProcessLimiter.cs
            └── StartupTaskService.cs