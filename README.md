<!-- img src="https://img.shields.io/badge/version-1.0.0-blue" -->
<!-- img src="https://img.shields.io/badge/license-MIT-green" -->

<div align="center">

# 🛡️ ACE-Monitor

腾讯ACE进程监控限制工具

*[English](./docs/README_EN.md) | 中文*

</div>

---

## 📋 简介

ACE-Monitor 是一款用于监控并限制腾讯ACE（Anti-Cheat Expert）游戏反作弊系统进程的工具。

腾讯ACE反作弊系统在被运行的游戏启动后，会在后台持续扫描文件，导致：
- 磁盘持续高读写
- 游戏帧率下降
- 鼠标操作延迟
- 固态硬盘寿命缩短

本工具通过降低进程优先级的方式，在保证游戏正常运行的前提下，减少ACE对系统资源的占用。

---

## ✨ 功能特性

| 功能 | 说明 |
|------|------|
| 🔍 **自动检测** | 每30秒自动扫描ACE相关进程 |
| ⚡ **优先级限制** | 将进程设为最低优先级 (Idle) |
| 🎯 **CPU亲和性限制** | 可选：限制进程只使用1个CPU核心 |
| 📝 **日志记录** | 详细记录限制操作到日志文件 |
| 🔄 **定时循环** | 持续监控，检测到自动处理 |
| 🎨 **简易操作** | 一键启动/停止，无需复杂配置 |

---

## 📦 监控的进程

- `SGuard64.exe` - ACE主进程
- `SGuardSvc64.exe` - ACE服务进程
- `ACE-GuardClient.exe` - ACE客户端
- `TPBootLoader.exe` - 腾讯安全组件
- `TPHelper.exe` - 腾讯助手

---

## 🚀 快速开始

### 使用前提

> ⚠️ **必须以管理员身份运行！**

### 方法一：双击运行（推荐）

```
1. 右键 start.bat → 以管理员身份运行
2. 双击打开
3. 监控服务自动启动
```

### 方法二：PowerShell运行

```powershell
# 进入目录
cd D:\ACE-Monitor

# 直接运行
.\Monitor-ACE.ps1

# 静默模式（无控制台窗口）
.\Monitor-ACE.ps1 -Silent

# 启用CPU限制
.\Monitor-ACE.ps1 -CpuLimit
```

---

## 📖 命令行参数

| 参数 | 简写 | 说明 | 默认值 |
|------|------|------|--------|
| `-Interval` | `-Int` | 检测间隔（秒） | 30 |
| `-Silent` | `-Sl` | 静默模式，不显示控制台输出 | - |
| `-CpuLimit` | `-CP` | 启用CPU亲和性限制 | - |
| `-Stop` | - | 停止监控服务 | - |

### 使用示例

```powershell
# 修改检测间隔为60秒
.\Monitor-ACE.ps1 -Interval 60

# 静默模式 + CPU限制
.\Monitor-ACE.ps1 -Silent -CpuLimit

# 停止监控服务
.\Monitor-ACE.ps1 -Stop
```

---

## 📂 项目结构

```
ACE-Monitor/
├── Monitor-ACE.ps1    # 主脚本
├── start.bat          # 一键启动（后台运行）
├── stop.bat           # 停止监控
├── README.md          # 本文档
├── LICENSE            # MIT许可证
├── docs/
│   ├── README_EN.md   # 英文版文档
│   └── CHANGELOG.md   # 更新日志
└── Logs/              # 日志目录
    └── ACE_Monitor_YYYYMMDD.log
```

---

## ❓ 常见问题

### Q: 为什么不生效？

**A:** 请确认以**管理员身份**运行脚本。普通权限无法修改进程优先级。

### Q: 如何查看日志？

**A:** 
- 日志位置：`Logs\ACE_Monitor_YYYYMMDD.log`
- 实时查看：`Get-Content Logs\*.log -Tail 20 -Wait`

### Q: 会不会导致游戏被封？

**A:** 不会。本工具只是降低进程优先级，不修改游戏文件，不影响反作弊功能正常工作。

### Q: 可以开机自启吗？

**A:** 可以。将 `start.bat` 创建一个快捷方式，放到以下位置：
- `C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup`（开机自启动）
- 或使用任务计划程序

---

## ⚠️ 免责声明

1. 本工具仅用于个人系统资源管理
2. 使用本工具需自行承担风险
3. 禁止用于任何商业或恶意目的
4. 作者不对任何因此工具产生的后果负责

---

## 📝 更新日志

详见 [CHANGELOG.md](./docs/CHANGELOG.md)

---

## 📜 许可证

本项目基于 [MIT](./LICENSE) 许可证开源。

---

## 🙏 致谢

如果觉得有用，欢迎 ⭐ Star 支持！

---

<div align="center">

*Created with ❤️*

</div>