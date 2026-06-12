namespace ACE.Monitor.App.Models;

public sealed class MonitorSettings
{
    public int IntervalSeconds { get; set; } = 30;
    public bool EnableCpuLimit { get; set; } = true;
    public bool EnablePriorityLimit { get; set; } = true;
    public long CpuAffinityMask { get; set; } = 1;
    public string[] TargetProcessNames { get; set; } =
    [
        "SGuard64",
        "SGuardSvc64",
        "ACE-GuardClient"
    ];
}
