<!-- img src="https://img.shields.io/badge/version-1.0.0-blue" -->
<!-- img src="https://img.shields.io/badge/license-MIT-green" -->

<div align="center">

# 🛡️ ACE-Monitor

Tencent ACE Process Monitor & Limiter

</div>

---

## 📋 Introduction

ACE-Monitor is a tool designed to monitor and limit the process priority of Tencent ACE (Anti-Cheat Expert) anti-cheat system.

When Tencent ACE anti-cheat system runs with a game, it continuously scans files in the background, causing:
- Continuous high disk read/write
- Game frame rate drops
- Mouse operation delays
- Shortened SSD lifespan

This tool reduces system resource usage by lowering process priority while ensuring normal game operation.

---

## ✨ Features

| Feature | Description |
|---------|-------------|
| 🔍 **Auto Detection** | Scans for ACE processes every 30 seconds |
| ⚡ **Priority Limiting** | Sets process to lowest priority (Idle) |
| 🎯 **CPU Affinity** | Optional: Limit process to single CPU core |
| 📝 **Logging** | Detailed operation logs |
| 🔄 **Continuous Monitor** | Auto-detect and process when found |
| 🎨 **Easy to Use** | One-click start/stop, no complex config |

---

## 📦 Monitored Processes

- `SGuard64.exe` - ACE Main Process
- `SGuardSvc64.exe` - ACE Service Process
- `ACE-GuardClient.exe` - ACE Client
- `TPBootLoader.exe` - Tencent Security Component
- `TPHelper.exe` - Tencent Helper

---

## 🚀 Quick Start

### Prerequisites

> ⚠️ **Must run as Administrator!**

### Method 1: Double-click to Run (Recommended)

```
1. Right-click start.bat → Run as administrator
2. Double-click to open
3. Monitor service starts automatically
```

### Method 2: PowerShell

```powershell
# Enter directory
cd D:\ACE-Monitor

# Run directly
.\Monitor-ACE.ps1

# Silent mode (no console window)
.\Monitor-ACE.ps1 -Silent

# Enable CPU limit
.\Monitor-ACE.ps1 -CpuLimit
```

---

## 📖 Command Line Parameters

| Parameter | Short | Description | Default |
|-----------|-------|-------------|---------|
| `-Interval` | `-Int` | Detection interval (seconds) | 30 |
| `-Silent` | `-Sl` | Silent mode, no console output | - |
| `-CpuLimit` | `-CP` | Enable CPU affinity limit | - |
| `-Stop` | - | Stop monitor service | - |

### Usage Examples

```powershell
# Change detection interval to 60 seconds
.\Monitor-ACE.ps1 -Interval 60

# Silent mode + CPU limit
.\Monitor-ACE.ps1 -Silent -CpuLimit

# Stop monitor service
.\Monitor-ACE.ps1 -Stop
```

---

## 📂 Project Structure

```
ACE-Monitor/
├── Monitor-ACE.ps1    # Main Script
├── start.bat          # One-click start (run in background)
├── stop.bat           # Stop monitor
├── README.md          # Chinese Documentation
├── LICENSE            # MIT License
├── docs/
│   ├── README_EN.md   # English Documentation
│   └── CHANGELOG.md   # Changelog
└── Logs/              # Log directory
    └── ACE_Monitor_YYYYMMDD.log
```

---

## ❓ FAQ

### Q: Why isn't it working?

**A:** Please confirm you are running as **Administrator**. Normal permissions cannot modify process priority.

### Q: How to view logs?

**A:** 
- Log location: `Logs\ACE_Monitor_YYYYMMDD.log`
- Real-time view: `Get-Content Logs\*.log -Tail 20 -Wait`

### Q: Will this cause my game to be banned?

**A:** No. This tool only lowers process priority, does not modify game files, and does not affect anti-cheat functionality.

### Q: Can I auto-start on boot?

**A:** Yes. Create a shortcut of `start.bat` and place it in:
- `C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup`

---

## ⚠️ Disclaimer

1. This tool is for personal system resource management only
2. Use at your own risk
3. Prohibited for any commercial or malicious purposes
4. Author is not responsible for any consequences arising from this tool

---

## 📝 Changelog

See [CHANGELOG.md](./CHANGELOG.md)

---

## 📜 License

This project is licensed under the [MIT](./LICENSE) License.

---

## 🙏 Acknowledgments

If you find this useful, please ⭐ Star to show your support!

---

<div align="center">

*Created with ❤️*

</div>