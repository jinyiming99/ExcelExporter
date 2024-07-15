namespace ConsoleApp1.DebugLog;

public interface IDebugFunction
{
    Action<string> LogAction { get; }
    
    Action<string> logWarningAction { get; }
    
    Action<string> logErrorAction { get; }
}