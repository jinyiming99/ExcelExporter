using System;
using System.Diagnostics;
using ConsoleApp1.DebugLog;
using GameFrameWork.Util;


namespace GameFrameWork.DebugTools
{
    /// <summary>
    /// debug用的对外的
    /// </summary>
    public static class DebugHelper
    {
        private static IDebugFunction _debugFunction;

        private static void CreateFunction()
        {
            if (_debugFunction != null)
                return;
            var types = AssemblyTool.FindTypeBase(typeof(IDebugFunction));
            if (types is not null && types.Length > 0)
            {
                foreach (var type in types)
                {
                    if (!type.IsAbstract && !type.IsInterface)
                    {
                        var obj = Activator.CreateInstance(type);
                        if (obj != null)
                        {
                            _debugFunction = obj as IDebugFunction;
                            break;
                        }
                    }
                }
            }
        }
        [Conditional("LOG_OUT")]
        public static void Log(string str)
        {
            if (string.IsNullOrEmpty(str))
                return;
            CreateFunction();
            var logStr = $"log: time = {System.DateTime.Now.ToString()}, log = {str}";
            _debugFunction.LogAction(logStr);

        }
        /// <summary>
        /// 为了防止在外面组装string，控制不了
        /// </summary>
        /// <param name="strFunc"></param>
        /// <param name="type"></param>
        [Conditional("LOG_OUT")]
        public static void Log(Func<string> strFunc)
        {
            string str = string.Empty;
            if (strFunc != null)
                str = strFunc();
            else
                return;
            Log(str);
        }
        [Conditional("LOG_OUT")]
        public static void LogWarning(string str)
        {
            if (string.IsNullOrEmpty(str))
                return;
            CreateFunction();
            var logStr = $"warning: time = {System.DateTime.Now.ToString()}, log = {str}";
            _debugFunction.logWarningAction(logStr);

        }
        [Conditional("LOG_OUT")]
        public static void LogWarning(Func<string> strFunc)
        {
            string str = string.Empty;
            if (strFunc != null)
                str = strFunc();
            else
                return;

            LogWarning(str);
        }

        [Conditional("LOG_OUT")]
        public static void LogError(string str)
        {
            if (string.IsNullOrEmpty(str))
                return;
            CreateFunction();
            var logStr = $"Error time = {System.DateTime.Now.ToString()}, log = {str}";
            _debugFunction.logErrorAction(logStr);
        }
        [Conditional("LOG_OUT")]
        public static void LogError(Func<string> strFunc)
        {
            string str = string.Empty;
            if (strFunc != null)
                str = strFunc();
            else
                return;
            LogError(str);
        }

    }
}


