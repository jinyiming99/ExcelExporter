namespace ConsoleApp1;

public class ExportData
{
    private static ExportData _instance;
    public static ExportData Instance => _instance ??= new ExportData();
    private Dictionary<string,ClassData> _classData = new Dictionary<string, ClassData>();
    
    public void Add(string name,ClassData data)
    {
        _classData.Add(name,data);
    }

    public void Check()
    {
        
    }
}