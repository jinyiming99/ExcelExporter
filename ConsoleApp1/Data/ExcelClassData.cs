using NPOI.SS.UserModel;

namespace ConsoleApp1;



public class ExcelClassData
{
    public string _name;
    public List<ExcelFieldData> _fields = new List<ExcelFieldData>();
    public ExcelFieldData _keyData = null;

    public ExcelFieldData KeyData
    {
        set => _keyData = value;
        get => _keyData;
    }
    
    public void AddField(ExcelFieldData data)
    {
        _fields.Add(data);
    }
}