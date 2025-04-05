using System.Diagnostics;

namespace KamomeCAlgoExtensions.Debug;

public static class DebugHelper
{
    public static void LaunchDebugger(Action<string> logger)
    {
        logger("Launching debugger.");
        var debuggerLaunched = Debugger.Launch();
        if (debuggerLaunched is false) logger("Failed to launch the debugger.");
    }
}