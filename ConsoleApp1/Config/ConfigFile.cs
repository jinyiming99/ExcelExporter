using GameFrameWork.DebugTools;

namespace ConsoleApp1.Config;

public abstract class ConfigFile
{
    protected Dictionary<string, string> _dic = new Dictionary<string, string>();

    public static T Create<T>(string path) where T : ConfigFile, new()
    {
        
        if (!File.Exists(path))
            return null;
        
        T file = new T();
        using (StreamReader reader = new StreamReader(path))
        {
            List<string> lines = new List<string>();
            while (!reader.EndOfStream)
            {
                var str = reader.ReadLine();
                lines.Add(str);
            }
            if (!file.SetData(lines))
                return null;
        }
        return file;
    }

    public static void CreateRaw<T>(string path) where T : ConfigFile, new()
    {
        T t = new T();
        using (StreamWriter reader = new StreamWriter(path))
        {
            t.WriteRawData(reader);
        }
    }

    public bool SetData(List<string> args)
    {
        _dic.Clear();
        foreach (var arg in args)
        {
            var arr = arg.Split('=');
            if (arr.Length != 2 || string.IsNullOrEmpty(arr[1]))
            {
                if (string.IsNullOrEmpty(arr[1]))
                    DebugHelper.LogError($"${arr[0]} config is empty");
                return false;
            }
            _dic.Add(arr[0],arr[1]);
        }

        return true;
    }

    protected abstract void CheckData();
    protected abstract void WriteRawData(StreamWriter reader);

}