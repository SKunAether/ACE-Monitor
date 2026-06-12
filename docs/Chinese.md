<div align="center">

# 🛡️ ACE Monitor App

Windows 桌面版腾讯 ACE 进程监控限制工具

</div>

---

## 简介

ACE Monitor App 是原 PowerShell 脚本项目的桌面应用重构版。它会持续监控指定的 3 个腾讯 ACE 相关进程，并在发现后自动应用资源限制：

- 将目标进程优先级设置为 `Idle`
- 将目标进程 CPU 亲和性限制到单个 CPU 核心（核心 0）
- 记录所有扫描与限制操作日志
- 可通过界面开启/关闭监控
- 可添加到 Windows 开机自启，并以最小化状态自动开始监控

> ⚠️ 本程序需要管理员权限运行，否则可能无法修改进程优先级或 CPU 亲和性。

---

## 当前版本重点

本版本以 WPF 桌面应用为基准，重点保留：只监控 3 个进程、开机自启、最小化启动、批处理一键发布 exe。

| 模块 | 新版实现 |
|---|---|
| 启动/停止 | 在 UI 中点击“开始监控 / 停止” |
| 优先级限制 | 默认启用，设置为 Idle |
| 单核限制 | 默认启用，限制到 CPU 核心 0 |
| 开机自启 | UI 勾选后创建 Windows 任务计划程序任务，登录后以 `--minimized --autostart` 启动 |
| 日志 | `程序目录\Logs\ACE_Monitor_YYYYMMDD.log` |
| 权限 | `app.manifest` 要求管理员权限 |

---

## 监控的进程

- `SGuard64.exe`
- `SGuardSvc64.exe`
- `ACE-GuardClient.exe`

---

## 项目结构

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