using System.Collections;

namespace ConsoleApp1.DebugLog;

public class DebugFunction : IDebugFunction
{
    private DebugFile _debugFile = null;
    public DebugFunction()
    {
        _debugFile = new DebugFile();
    }
    public void Log(string str)
    {
        Console.WriteLine(str);
        _debugFile.WriteLog(str);
    }
    public void LogWarning(string str)
    {
        Console.WriteLine(str);
        _debugFile.WriteLog(str);
    }
    public void LogError(string str)
    {
        Console.WriteLine(str);
        _debugFile.WriteLog(str);
    }
    public Action<string> LogAction { get => Log;  }
    public Action<string> logWarningAction { get =>LogWarning;  }
    public Action<string> logErrorAction { get => LogError;  }
}