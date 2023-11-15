namespace ConsoleApp1;

public class ClassInfo
{
    public string _name;
    public FieldType _type;
}
public class ExcelData
{
    public Dictionary<string,ExcelClassData> _classData = new Dictionary<string, ExcelClassData>();
    
    public void Add(string name,ExcelClassData data)
    {
        _classData.Add(name,data);
    }
    
    public bool FindClassName(string name)
    {
        return _classData.ContainsKey(name);
    }
}