@echo off
chcp 65001 >nul 2>&1

echo ============================================
echo    ACE Monitor - Starting
echo ============================================

:: Check admin rights
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo [ERROR] Please run as Administrator!
    echo Right-click ^> Run as administrator
    pause
    exit /b 1
)

echo [OK] Admin verified
echo.

:: Get script directory
set "SCRIPT_DIR=%~dp0"
set "SCRIPT_DIR=%SCRIPT_DIR:~0,-1%"

:: Start monitor - keep running in background
start "ACE Monitor" cmd /c "cd /d "%SCRIPT_DIR%" && powershell -ExecutionPolicy Bypass -NoProfile -File Monitor-ACE.ps1 -Sl"

echo [OK] Monitor started in background
echo.
echo Check logs: Logs\ACE_Monitor_*.log
echo.
timeout /t 3 >nul
exit /b 0