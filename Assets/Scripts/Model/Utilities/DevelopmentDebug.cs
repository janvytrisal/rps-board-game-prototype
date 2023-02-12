/*
 * Author: Jan Vytrisal
 */

using System.Diagnostics;

/// <summary>
/// Debug logging for development purposes.
/// </summary>
/// <remarks>
/// To enable logs set ENABLE_LOGS as Scripting Define Symbol in Project Settings > Player.
/// When logging is disabled the method calls are not compiled.
/// </remarks>
public static class DevelopmentDebug
{
    [Conditional("ENABLE_LOGS")]
    public static void Log(string message)
    {
        UnityEngine.Debug.Log(message);
    }
    [Conditional("ENABLE_LOGS")]
    public static void LogWarning(string message)
    {
        UnityEngine.Debug.LogWarning(message);
    }
    [Conditional("ENABLE_LOGS")]
    public static void LogError(string message)
    {
        UnityEngine.Debug.LogError(message);
    }
}
