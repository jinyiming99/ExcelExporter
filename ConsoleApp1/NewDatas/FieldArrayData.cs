namespace ConsoleApp1.NewDatas;

public class FieldArrayData : BaseValue
{
    public List<FieldSturctValue> _values;
    private bool _isMultiType;

    private int _index;
    public FieldArrayData(CellData typeData,string name):base(FieldType.Array,DataType.Null,name)
    {
        var type = typeData.Txt.Replace("[]", "");
        if (type.Contains('{'))
        {
            type = type.Replace("{", "");
            type = type.Replace("}", "");
            var strs = type.Split(',');
            _values = new List<FieldSturctValue>(strs.Length);
            foreach (var str in strs)
            {
                var arrs = str.Split(":");
                var keyName = arrs[0];
                var keyType = arrs[1];
                _values.Add(new FieldSturctValue(FieldSturctValue.GetStruct(keyType),keyName));
            }

            _isMultiType = true;
        }
        else
        {
            _dataType = FieldSturctValue.GetStruct(type);
            _isMultiType = false;
        }
    }

    public override async Task<string> ClassStr()
    {
        if (_isMultiType)
        {
            StringWriter writer = new StringWriter();
            await writer.WriteLineAsync($"    public class CustomAttr{_index}");
            await writer.WriteLineAsync("    {");
        
            foreach (var str in _values)
            {
                await writer.WriteLineAsync($"        public {FieldSturctValue.GetTypeString(str.DataType)} {str.Name};");
            }
        
            await writer.WriteLineAsync("    }");
            return writer.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    public override async Task<string> SturctStr()
    {
        if (_isMultiType)
        {
            return $"public List<CustomAttr{_index}> {_name.ToUpperFirst()};";
        }
        else
        {
            return $"public List<{FieldSturctValue.GetTypeString(_dataType)}> {_name.ToUpperFirst()};";
        }
    }

    public override async Task<string> GetData(int index)
    {
        if (_isMultiType)
        {
            return $"var {_name} = new List<CustomAttr{index}>();";
        }
        else
        {
            string str = string.Empty;
            if (_CellDatas.TryGetValue(index, out var data))
            {
                var arrs = data.Txt.Split(":");
                foreach (var arr in arrs)
                {
                    if (!string.IsNullOrEmpty(str))
                        str += ",";
                    str += $"{arr}" ;
                }
                
            }
            return $"{_name} = new List<{FieldSturctValue.GetTypeString(_dataType)}>()" + "{" + $"{str}" +"}";
        }
    
    }
}