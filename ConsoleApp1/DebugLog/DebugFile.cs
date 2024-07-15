namespace ConsoleApp1.DebugLog;

public class DebugFile
{
    private string _filePath = Directory.GetCurrentDirectory() + $"/Debug{DateTime.Now.ToString("yy-MM-dd--hh-mm")}.log";
    private StreamWriter _writer;

    public DebugFile()
    {
        CreateFile();
    }
    ~DebugFile()
    {
        Close();
    }
    
    public void CreateFile()
    {
        if (_writer == null)
        {
            _writer = new StreamWriter(_filePath);
            _writer.AutoFlush = true;
        }
    }



    public void WriteLog(string log)
    {
        _writer.WriteLine(log);
    }

    public void Save()
    {
        _writer.Flush();
    }

    public void Close()
    {
        _writer.Flush();
        _writer.Close();
    }
}