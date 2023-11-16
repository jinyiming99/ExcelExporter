namespace ConsoleApp1.NewDatas;

public class FieldSturctValue: BaseValue
{ 
    public FieldSturctValue(DataType dType,string name) : base(FieldType.Struct,dType,name)
    {
        _type = FieldType.Struct;
    }
    public override async Task<string> SturctStr()
    {
        switch (_dataType)
        {
            case DataType.Bool:
                return $"public bool {_name.ToUpperFirst()};";
            case DataType.Float:
                return $"public float {_name.ToUpperFirst()};";
            case DataType.Int:
                return $"public int {_name.ToUpperFirst()};";
            case DataType.Short:
                return $"public short {_name.ToUpperFirst()};";
            case DataType.String:
                return $"public string {_name.ToUpperFirst()};";
        }
        return string.Empty;
    }

    public override async Task<string> GetData(int index)
    {
        if (_CellDatas.TryGetValue(index, out var data))
        {
            return $"{_name.ToUpperFirst()} = {await GetValueData(data.Txt)}";
        };
        return string.Empty;
    }

    public async Task<string> GetDefault(DataType type)
    {
        switch (type)
        {
            case DataType.Bool:
                return "false";
            case DataType.Float:
                return "0";
            case DataType.Int:
                return "0";
            case DataType.Short:
                return "0";
            case DataType.String:
                return "\"\"";
            
        }
        return string.Empty;
    }

    public static bool IsStruct(CellData Type)
    {
        if (Type.Txt.Equals("int") || Type.Txt.Equals("int32"))
            return true;
        else if (Type.Txt.Equals("short") )
            return true;
        else if (Type.Txt.Equals("float") )
            return true;
        else if (Type.Txt.Equals("string") )
            return true;
        else if (Type.Txt.Equals("bool") )
            return true;
        return false;
    }
    public static DataType GetStruct(string Type)
    {
        if (Type.Equals("int") || Type.Equals("int32"))
            return DataType.Int;
        else if (Type.Equals("short") )
            return DataType.Short;
        else if (Type.Equals("float") )
            return DataType.Float;
        else if (Type.Equals("string") )
            return DataType.String;
        else if (Type.Equals("bool") )
            return DataType.Bool;
        return DataType.Null;
    }

    public static string GetTypeString(DataType type)
    {
        switch (type)
        {
            case DataType.Bool:
                return "bool";
            case DataType.Float:
                return "float";
            case DataType.Int:
                return "int";
            case DataType.Short:
                return "short";
            case DataType.String:
                return "string";
            default:
                return string.Empty;
        }
    }
    public override async Task<string> GetValueData(string txt)
    {
        switch (_dataType)
        {
            case ConsoleApp1.DataType.String:
                return $"\"{txt}\"";
            default:
            {
                if (string.IsNullOrEmpty(txt))
                    return $"{await GetDefault(_dataType)}";
                return $"{txt}"; 
            }
        }

    }
}