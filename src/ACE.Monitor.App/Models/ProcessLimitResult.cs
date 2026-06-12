namespace ACE.Monitor.App.Models;

public sealed record ProcessLimitResult(
    string ProcessName,
    int ProcessId,
    bool PriorityApplied,
    bool CpuAffinityApplied,
    string? Error);
