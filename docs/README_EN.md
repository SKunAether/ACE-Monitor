

<div align="center">

# 🛡️ ACE-Monitor

**ACE Process Monitor & Limiter – Windows Desktop App**

*English | [中文](../README.md)*

</div>

---

## 📋 Introduction

ACE-Monitor is a **Windows desktop application** (WPF) that monitors and limits processes of the Tencent ACE (Anti-Cheat Expert) anti-cheat system.

When a protected game runs, ACE continuously scans files in the background, leading to:
- High disk read/write activity
- Lower game frame rates
- Increased mouse input latency
- Reduced SSD lifespan

This tool significantly reduces ACE’s resource usage by **lowering process priority** and **restricting CPU affinity**, while keeping the game fully functional.

> ⚠️ **Administrator privileges are required** – otherwise priority/affinity changes will fail.

---

## ✨ Features

| Feature                     | Description                                                  |
| --------------------------- | ------------------------------------------------------------ |
| 🖥️ **GUI**                   | Simple WPF interface, one‑click start/stop                   |
| 🔍 **Auto‑detection**        | Customizable scan interval (default 30s), automatically finds ACE processes |
| ⚡ **Priority limiting**     | Sets target process priority to `Idle` (lowest)              |
| 🎯 **CPU affinity limiting** | Optional: restrict process to a single CPU core (core 0)     |
| 🚀 **Startup with Windows**  | GUI toggle creates a scheduled task – auto‑starts minimized with admin rights |
| 📝 **Logging**               | All actions logged to `Logs` directory, live preview in UI   |
| 🔄 **Continuous monitoring** | Limits are reapplied automatically whenever target processes appear |
| ⚙️ **Flexible settings**     | Adjust scan interval, enable/disable priority or CPU limits on the fly |

---

## 📦 Monitored Processes

- `SGuard64.exe`
- `SGuardSvc64.exe`
- `ACE-GuardClient.exe`

> The tool monitors **only these three ACE‑related processes**. It does not interfere with other system processes or game executables.

---

## 🚀 Quick Start

### Option 1: Use the Pre‑built Installer (recommended for most users)

1. Download the latest `ACE-Monitor-Setup.exe` from [Releases](https://github.com/your-repo-url/releases)
2. Right‑click the installer → **Run as administrator** and follow the setup wizard
3. Launch ACE-Monitor from the Start Menu or desktop shortcut
4. **Always run the program as administrator** (right‑click → Run as administrator)
5. In the UI click **Start Monitoring**, and toggle **Startup with Windows** if desired

### Option 2: Build from Source (for developers)

#### Requirements
- Windows 10 / Windows 11
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

#### Build & Run
```bash
# Clone the repository
git clone https://github.com/your-username/ACE-Monitor-App.git
cd ACE-Monitor-App

# Build the project
dotnet build src/ACE.Monitor.App/ACE.Monitor.App.csproj -c Release

# Run as administrator
# Executable located at: src/ACE.Monitor.App/bin/Release/net8.0-windows/ACE-Monitor.exe
```

#### One‑click publish (self‑contained EXE)

Double‑click `build-release.bat` in the root folder. A single‑file `ACE-Monitor.exe` will be generated in `publish\win-x64` – no .NET runtime installation needed on the target machine.

------

## 🖥️ Usage

### UI Layout

- **Control Panel** – set scan interval (seconds), toggle priority limiting, toggle CPU limiting, enable/disable startup with Windows
- **Status Indicator** – shows current monitoring state (running / stopped)
- **Statistics Cards** – shows processes found in last scan, last scan time, and active limiting strategy
- **Live Log** – real‑time display of monitoring actions and errors

### Basic Operations

1. **Start Monitoring** – click “Start Monitoring”. The program will scan at the set interval and apply limits to any found ACE processes.
2. **Stop Monitoring** – click “Stop”. The monitoring loop stops.
3. **Startup with Windows** – check the box to create a Windows scheduled task (requires admin rights). After a reboot, the program will start minimized and automatically begin monitoring.

> Tip: The startup feature uses Task Scheduler. You must run the program **as administrator at least once** to create the task.

### Command‑line arguments (advanced)

The program supports the following arguments:

- `--minimized` – start minimized to the taskbar
- `--autostart` – start monitoring automatically (used together with `--minimized` for silent startup)

The scheduled task adds `--minimized --autostart` by default, enabling seamless background monitoring after login.

------

## 📂 Project Structure

text

```
ACE-Monitor-App/
├── README.md                    # Chinese documentation
├── LICENSE                      # License file
├── .gitignore                   # Git ignore rules
├── ACE-Monitor-App.sln          # Solution file
├── build-release.bat            # One‑click publish script
├── docs/
│   └── README_EN.md             # English documentation (this file)
└── src/
    └── ACE.Monitor.App/
        ├── ACE.Monitor.App.csproj
        ├── app.manifest         # Admin rights manifest
        ├── App.xaml / .cs       # Application entry
        ├── MainWindow.xaml / .cs # Main UI logic
        ├── Models/               # Data models
        └── Services/             # Core services (logging, monitoring, limiting, startup)
```



------

## ❓ Frequently Asked Questions

### Q: The program cannot change process priority – permission error?

**A:** You must **run as administrator**. Right‑click `ACE-Monitor.exe` → “Run as administrator”, or set the executable to always require admin rights in its properties.

### Q: The startup toggle doesn't work?

**A:**

- Make sure you have **run the program as administrator at least once** – otherwise it cannot create the scheduled task.
- Open Task Scheduler and verify that a task named `ACE-Monitor-App` exists. If not, check the program's log for errors.
- If the task exists but does not run automatically, ensure the trigger is set to “At log on”.

### Q: Can this cause a game ban?

**A:** Absolutely not. The tool only uses standard Windows APIs to lower priority and restrict CPU affinity. It does not modify game files, inject code, or interfere with the anti‑cheat system in any way. All operations are purely system‑level and fully compliant with Windows resource management.

### Q: Where are the logs stored?

**A:** Log files are saved in `Logs\ACE_Monitor_YYYYMMDD.log` under the program’s directory. The UI also provides an “Open Logs Folder” button for quick access.

### Q: How do I completely uninstall?

**A:**

- If you enabled startup with Windows, first **uncheck the startup box** in the UI – this will remove the scheduled task.
- Then simply delete the program folder (you may also delete the `Logs` subfolder).

------

## ⚠️ Disclaimer

1. This tool is intended for **personal system resource management only** – it is not targeted against any specific game or company.
2. Use at your own risk. The author is not responsible for any game anomalies, system issues, or account penalties resulting from the use of this tool.
3. This tool does not modify game files nor does it inject into any anti‑cheat process.
4. Commercial use or malicious purposes are strictly prohibited.

------

## 📝 Changelog

See [CHANGELOG.md](https://./CHANGELOG.md) (if available)

------

## 📜 License

This project is licensed under a **custom Non‑Commercial License**.
Personal use and source code modification are permitted, but **commercial use is forbidden**.
See the [LICENSE](https://../LICENSE) file for details.

------

## 🙏 Acknowledgements

If you find this tool useful, please ⭐ Star the repository!

------

<div align="center">

*Created with ❤️*

</div> 
