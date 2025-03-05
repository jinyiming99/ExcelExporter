namespace ConsoleApp1.NewDatas;

public class FieldArrayData : BaseValue
{
    public List<FieldSturctValue> _values;
    private bool _isMultiType;
    private static int Count = 0;
    private int _index;
    public FieldArrayData(CellData typeData,string name):base(FieldType.Array,DataType.Null,name)
    {
        _index = Count++;
        string type = typeData.Txt;
        if (type.Contains("arr:") )
            type = typeData.Txt.Replace("arr:", "");
        else if (type.Contains("json:"))
            type = typeData.Txt.Replace("json:", "");
        //{a:int,b:string}[]
        if (type.Contains('['))
        {
            type = type.Replace("[", "");
            type = type.Replace("]", "");
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
            _values = new List<FieldSturctValue>();
            _values.Add(new FieldSturctValue(_dataType,string.Empty));
            _isMultiType = false;
        }
    }

    public override async Task<string> ClassStr()
    {
        if (_isMultiType)
        {
            StringWriter writer = new StringWriter();
            await writer.WriteLineAsync($"public class CustomAttr{_index}");
            await writer.WriteLineAsync("{");
        
            foreach (var str in _values)
            {
                await writer.WriteLineAsync($"   public {FieldSturctValue.GetTypeString(str.DataType)} {str.Name};");
            }
        
            await writer.WriteLineAsync("}");
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
            StringWriter writer = new StringWriter();
            await writer.WriteAsync($"{_name} = new List<CustomAttr{_index}>" + "{");
            if (_CellDatas == null)
            {
                Console.WriteLine($"{_name} _CellDatas is null");
            }
            //1,a:2,b
            //foreach (var kv in _CellDatas)
            {
                var kv = _CellDatas[index];
                var tempStr = kv.Txt.Replace("[", "");
                tempStr = tempStr.Replace("]", "");
                var arrs = tempStr.Split(":");
                
                for (int i = 0; i < arrs.Length; i++)
                {
                    if (i != 0)
                        await writer.WriteAsync(",");
                    await writer.WriteAsync($"new CustomAttr{_index}"+"{");
                    var values = arrs[i].Split(",");
                    for (int index1 = 0; index1 < values.Length; index1++)
                    {
                        var fieldInfo = _values[index1];
                        if (index1 != 0)
                            await writer.WriteAsync(",");
                        await writer.WriteAsync($"{fieldInfo.Name} ={await fieldInfo.GetValueData(values[index1])}");
                    }
                    await writer.WriteAsync("}");
                }
            }
            await writer.WriteAsync("}");
            return writer.ToString();
        }
        else
        {
            ///数组分裂符号;
            string str = string.Empty;
            if (_CellDatas.TryGetValue(index, out var data))
            {
                var arrs = data.Txt.Split(";");
                foreach (var arr in arrs)
                {
                    if (!string.IsNullOrEmpty(str))
                        str += ",";
                    str += $"{await _values[0].GetValueData(arr)}" ;
                }
                
            }
            return $"{_name} = new List<{FieldSturctValue.GetTypeString(_dataType)}>()" + "{" + $"{str}" +"}";
        }
    
    }
}