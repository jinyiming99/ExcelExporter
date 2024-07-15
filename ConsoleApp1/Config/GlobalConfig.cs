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
            _inPath = Directory.GetCurrentDirectory();
        } 
        
        if (_dic.TryGetValue("outPath", out _outPath) == false)
        {
            _outPath = Directory.GetCurrentDirectory();
        } 
        if (_dic.TryGetValue("isClient", out var client))
        {
            _isClient = bool.Parse(client);
        } 
        
        if (_dic.TryGetValue("isServer", out var server))
        {
            _isServer = bool.Parse(server);
        } 
        if (_dic.TryGetValue("nameSpace", out _nameSpace) == false)
        {
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