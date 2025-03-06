namespace ConsoleApp1.Config;

public class GlobalConfig : ConfigFile
{
    public string _inPath = string.Empty;
    public string _outPath = string.Empty;
    public bool _isClient = false;
    public bool _isServer = false;
    public string _nameSpace = string.Empty;
    protected override void CheckData()
    {
        if (_dic.TryGetValue("inPath", out _inPath) == false)
        {
            if (string.IsNullOrEmpty(_inPath))
                _inPath = Directory.GetCurrentDirectory();
        } 
        
        if (_dic.TryGetValue("outPath", out _outPath) == false)
        {
            if (string.IsNullOrEmpty(_outPath))
                _outPath = Directory.GetCurrentDirectory();
        } 
        if (_dic.TryGetValue("isClient", out var client))
        {
            if (!string.IsNullOrEmpty(client))
            {
                if (!bool.TryParse(client, out _isClient))
                    Console.WriteLine("isClient参数错误");
            }
        } 
        
        if (_dic.TryGetValue("isServer", out var server))
        {
            if (!string.IsNullOrEmpty(server))
            {
                if (!bool.TryParse(server, out _isServer))
                    Console.WriteLine("isServer参数错误");
            }
        } 
        if (_dic.TryGetValue("nameSpace", out _nameSpace))
        {
            if (string.IsNullOrEmpty(_nameSpace) || string.IsNullOrWhiteSpace(_nameSpace))
                _nameSpace = "Config";
        } 
    }

    protected override void WriteRawData(StreamWriter reader)
    {
        reader.WriteLine("inPath=");
        reader.WriteLine("outPath=");
        reader.WriteLine("isClient=");
        reader.WriteLine("isServer=");
        reader.WriteLine("nameSpace=");
    }
}