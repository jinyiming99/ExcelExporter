namespace ConsoleApp1.NewDatas;

public class BaseValue
{
    protected FieldType _type;
    public FieldType Type => _type;
    
    protected DataType _dataType;
    public DataType DataType => _dataType;
    protected string _name;
    public string Name => _name;

    protected string _className;
    public string ClassName => _className;
    
    public Dictionary<int, CellData> _CellDatas;
    public BaseValue(FieldType fType,DataType dType,string name,string className)
    {
        _type = fType;
        _dataType = dType;
        _className = className;
        _name = name.ToUpperFirst();
    }

    public virtual async Task<string> ClassStr()
    {
        return string.Empty;
    }
    public virtual async Task<string> SturctStr()
    {
        return string.Empty;
    }
    
    public virtual async Task<string> GetData(int index)
    {
        return string.Empty;
    }

    public virtual async Task<string> GetValueData(string txt)
    {
        return txt;
    }
}