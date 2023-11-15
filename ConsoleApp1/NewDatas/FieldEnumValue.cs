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

        var list = await new Task<List<string>>(() =>
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
        });
        
        foreach (var str in list)
        {
            await writer.WriteLineAsync($"  {str.ToUpperFirst()},");
        }
        
        await writer.WriteLineAsync("}");
        return writer.ToString();
    }

    public FieldEnumValue(string eName,string name) : base(FieldType.Enum,DataType.Null,name)
    {
        _type = FieldType.Enum;
        _name = eName;
    }
}