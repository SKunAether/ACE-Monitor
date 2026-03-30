# 更新日志 CHANGELOG

All notable changes to this project will be documented in this file.

---

## [1.0.0] - 2026-03-30

### ✨ 新增功能
- 初始版本发布
- 自动检测腾讯ACE相关进程
- 降低进程优先级到Idle
- 支持CPU亲和性限制
- 完整的日志记录功能
- 一键启动/停止脚本

### 📋 监控进程列表
- SGuard64.exe
- SGuardSvc64.exe
- ACE-GuardClient.exe
- TPBootLoader.exe
- TPHelper.exe

### 🎯 核心功能
| 功能 | 说明 |
|------|------|
| 自动检测 | 每30秒检测一次 |
| 优先级限制 | 设为Idle（最低） |
| CPU限制 | 可选，限制单核心 |
| 日志记录 | 输出到Logs目录 |

---

## 更新历史

### [计划中]
- [ ] 添加开机自启动支持
- [ ] 添加桌面托盘图标
- [ ] 添加Web管理界面

---

*更多版本将陆续添加...*