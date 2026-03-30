#Requires -Version 5.1

param(
    [int]$Interval = 30,
    [switch]$Silent,
    [switch]$CpuLimit,
    [switch]$Stop
)

$TargetProcesses = @("SGuard64", "SGuardSvc64", "ACE-GuardClient", "TPBootLoader", "TPHelper")
$LogDir = "$PSScriptRoot\Logs"
$LogFile = Join-Path $LogDir ("ACE_Monitor_" + (Get-Date -Format "yyyyMMdd") + ".log")
$Running = $true

$IsAdmin = ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)

function W {
    param([string]$m)
    $t = Get-Date -Format "HH:mm:ss"
    $e = "[$t] $m"
    Add-Content -Path $LogFile -Value $e -Encoding UTF8
    if (-not $Silent) { Write-Host $e }
}

if (-not (Test-Path $LogDir)) {
    New-Item -ItemType Directory -Force -Path $LogDir | Out-Null
}

function Get-ACE {
    Get-Process -Name $TargetProcesses -ErrorAction SilentlyContinue
}

function Limit-Process {
    param($p)
    try {
        if ($p.PriorityClass -ne [System.Diagnostics.ProcessPriorityClass]::Idle) {
            $p.PriorityClass = [System.Diagnostics.ProcessPriorityClass]::Idle
            W "Limited: $($p.Name) PID:$($p.Id) -> Idle"
        }
        if ($CpuLimit) {
            $p.ProcessorAffinity = [IntPtr]1
        }
    } catch {
        W "Error: $_"
    }
}

if ($Stop) {
    W "Stop command received"
    $ps = Get-Process powershell -EA SilentlyContinue | Where-Object { $_.CommandLine -match "Monitor-ACE" }
    $ps | Stop-Process -Force -EA SilentlyContinue
    W "Monitor stopped"
    exit 0
}

W "=========================================="
W "ACE Monitor Started"
W "Processes: $($TargetProcesses -join ', ')"
W "Interval: ${Interval}sec"
W "Admin: $IsAdmin"
W "=========================================="

$loopCount = 0
while ($Running) {
    $loopCount++
    try {
        $procs = Get-ACE
        if ($procs) {
            W "Found $($procs.Count) ACE process(es)"
            $procs | ForEach-Object { Limit-Process $_ }
        }
    } catch {
        W "Error in loop: $_"
    }
    
    if ($loopCount % 10 -eq 0) {
        W "Heartbeat - still running..."
    }
    
    Start-Sleep -Seconds $Interval
}