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
```

---

## 编译运行

### 环境要求

- Windows 10 / Windows 11
- .NET 8 SDK

### 编译发布

双击运行：

```bat
build-release.bat
```

或手动执行：

```powershell
dotnet publish src\ACE.Monitor.App\ACE.Monitor.App.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o publish\win-x64
```

生成文件：

```text
publish\win-x64\ACE-Monitor.exe
```

右键该文件，选择“以管理员身份运行”。

---

## 开机自启说明

界面中勾选“开机自启”后，程序会创建名为 `ACE-Monitor-App` 的 Windows 任务计划程序任务：

- 触发器：用户登录时
- 权限：最高权限运行
- 动作：启动当前 ACE-Monitor.exe

取消勾选会删除该任务。任务启动命令包含 `--minimized --autostart`，因此登录后会最小化并自动开始监控。

---

## 日志

日志目录位于程序运行目录下：

```text
Logs\ACE_Monitor_YYYYMMDD.log
```

界面中也会实时显示最近日志。

---

## 免责声明

1. 本工具仅用于个人系统资源管理。
2. 使用本工具需自行承担风险。
3. 本工具不会修改游戏文件，也不会注入游戏或反作弊进程。
4. 作者不对任何因此工具产生的后果负责。

---

## 许可证

自定非商业许可证


如果觉得有用，欢迎 ⭐ Star 支持！
