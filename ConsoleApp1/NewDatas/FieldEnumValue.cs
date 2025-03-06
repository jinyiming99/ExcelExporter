namespace ConsoleApp1.NewDatas;

public class FieldEnumValue : BaseValue
{
    private string _enumName;
    public string EnumName => _enumName;
    public override async Task<string> SturctStr()
    {
        return $"public {_enumName.ToUpperFirst()} {_name.ToUpperFirst()};";
    }

    public override async Task<string> ClassStr()
    {
        StringWriter writer = new StringWriter();
        await writer.WriteLineAsync($"public enum {_enumName.ToUpperFirst()}");
        await writer.WriteLineAsync("{");

        var list = await GetList();
        
        foreach (var str in list)
        {
            await writer.WriteLineAsync($"  {str.ToUpperFirst()},");
        }
        
        await writer.WriteLineAsync("}");
        return writer.ToString();
    }

    public override async Task<string> GetData(int index)
    {
        return $"{_name.ToUpperFirst()} = {_enumName.ToUpperFirst()}.{_CellDatas[index].Txt.ToUpperFirst()}";
    }

    private async Task<List<string>> GetList()
    {
        Dictionary<string,int> dic = new Dictionary<string, int>();
        foreach (var cell in _CellDatas)
        {
            if (cell.Value == null) continue;
            if (string.IsNullOrEmpty(cell.Value.Txt)|| string.IsNullOrWhiteSpace(cell.Value.Txt))
                continue;
            dic.TryAdd(cell.Value.Txt,0);
        }
        return dic.Keys.ToList();
    }
    
    

    public FieldEnumValue(string eName,string name,string className) : base(FieldType.Enum,DataType.Null,name,className)
    {
        _type = FieldType.Enum;
        _enumName = eName;
    }
}