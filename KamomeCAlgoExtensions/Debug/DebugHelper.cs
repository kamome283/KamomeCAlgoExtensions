using System.Diagnostics;

namespace KamomeCAlgoExtensions.Debug;

public static class DebugHelper
{
    public static void LaunchDebugger(Action<string> logger, TimeSpan delay)
    {
        logger("Launching debugger.");
        var debuggerLaunched = Debugger.Launch();
        if (debuggerLaunched is false)
        {
            logger("Failed to launch the debugger.");
            return;
        }

        var delayTask = Task.Delay(delay);
        delayTask.Wait();
    }
}