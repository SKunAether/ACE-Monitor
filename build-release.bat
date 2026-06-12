@echo off
chcp 65001 >nul 2>&1
cd /d "%~dp0"
echo Building ACE Monitor App...
dotnet publish src\ACE.Monitor.App\ACE.Monitor.App.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o publish\win-x64
if %errorlevel% neq 0 (
  echo Build failed. Please install .NET 8 SDK.
  pause
  exit /b 1
)
echo Done: publish\win-x64\ACE-Monitor.exe
pause
