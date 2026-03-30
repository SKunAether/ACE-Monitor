@echo off
chcp 65001 >nul 2>&1

echo ============================================
echo    ACE Monitor - Stopping
echo ============================================

net session >nul 2>&1
if %errorLevel% neq 0 (
    echo [ERROR] Please run as Administrator!
    pause
    exit /b 1
)

echo Stopping monitor...
powershell -ExecutionPolicy Bypass -NoProfile -Command "Get-Process powershell -EA SilentlyContinue | Where-Object { \$_.CommandLine -match 'Monitor-ACE' } | Stop-Process -Force -EA SilentlyContinue"

echo [OK] Stopped
echo.
pause