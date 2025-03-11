namespace ConsoleApp1.NewDatas;

public class FieldArrayData : BaseValue
{
    public List<FieldSturctValue> _values;
    private bool _isMultiType;
    private static int Count = 0;
    private int _index;
    /// <summary>
    /// 如果是数组
    /// </summary>
    private bool _isArray = false;
    /// <summary>
    /// 如果是结构体
    /// </summary>
    private bool _isStruct = false;
    public FieldArrayData(CellData typeData,string name,string className):base(FieldType.Array,DataType.Null,name,className)
    {
        _index = Count++;
        string type = typeData.Txt;

        if (type.Contains("arr:"))
        {
            type = typeData.Txt.Replace("arr:", "");
            _isArray = true;
        }
        else if (type.Contains("json:"))
            type = typeData.Txt.Replace("json:", "");
        if (type.Contains("["))
            _isStruct = true;
        type = type.Replace("[", "");
        type = type.Replace("]", "");
        //Console.WriteLine($"FieldArrayData orgin {typeData.Txt } ; type:{type}");
        //{a:int,b:string}[]
        if (_isStruct)
        {
            var strs = type.Split(',');
            _values = new List<FieldSturctValue>(strs.Length);
            foreach (var str in strs)
            {
                var arrs = str.Split(":");
                var keyName = arrs[0];
                var keyType = arrs[1];
                _values.Add(new FieldSturctValue(FieldSturctValue.GetStruct(keyType),keyName,className));
            }

            _dataType = DataType.Struct;
            _isMultiType = true;
        }
        else
        {
            _dataType = FieldSturctValue.GetStruct(type);
            _values = new List<FieldSturctValue>();
            _values.Add(new FieldSturctValue(_dataType,string.Empty,className));
            _isMultiType = false;
        }
    }

    public override async Task<string> ClassStr()
    {
        if (_isMultiType)
        {
            StringWriter writer = new StringWriter();
            await writer.WriteLineAsync($"   public class {_name.ToUpperFirst()}CustomAttr");
            await writer.WriteLineAsync("       {");
        
            foreach (var str in _values)
            {
                await writer.WriteLineAsync($"          public {FieldSturctValue.GetTypeString(str.DataType)} {str.Name};");
            }
        
            await writer.WriteLineAsync("       }");
            return writer.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    public override async Task<string> SturctStr()
    {
        if (_isStruct && _isArray)
        {
            return $"public List<{_name.ToUpperFirst()}CustomAttr> {_name.ToUpperFirst()};";
        }
        else if (_isArray)
        {
            return $"public List<{FieldSturctValue.GetTypeString(_dataType)}> {_name.ToUpperFirst()};";
        }
        else
        {
            return $"public {_name.ToUpperFirst()}CustomAttr {_name.ToUpperFirst()};";
        }
    }

    public override async Task<string> GetData(int index)
    {
        if (_isStruct && _isArray)
        {
            StringWriter writer = new StringWriter();
            await writer.WriteAsync($"{_name} = new List<{_className.ToUpperFirst()}Config.{_name.ToUpperFirst()}CustomAttr>" + "{");
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
                var arrs = tempStr.Split(";");
                
                for (int i = 0; i < arrs.Length; i++)
                {
                    if (i != 0)
                        await writer.WriteAsync(",");
                    await writer.WriteAsync($"new {_className.ToUpperFirst()}Config.{_name.ToUpperFirst()}CustomAttr"+"{");
                    //Console.WriteLine(arrs[i]);
                    var values = arrs[i].Split(",");
                    for (int index1 = 0; index1 < values.Length; index1++)
                    {
                        //Console.WriteLine($"{_className.ToUpperFirst()} {_name.ToUpperFirst()}  {index1} values[index1] =  {values[index1]}");
                        if (string.IsNullOrEmpty(values[index1]))
                            continue;
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
        else if (_isArray)
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
        else
        {
            StringWriter writer = new StringWriter();
            var kv = _CellDatas[index];
            var tempStr = kv.Txt.Replace("[", "");
            tempStr = tempStr.Replace("]", "");
            
            await writer.WriteAsync($"{_name} = new {_className.ToUpperFirst()}Config.{_name.ToUpperFirst()}CustomAttr"+"{");
            var values = tempStr.Split(",");
            for (int index1 = 0; index1 < values.Length; index1++)
            {
                var fieldInfo = _values[index1];
                if (index1 != 0)
                    await writer.WriteAsync(",");
                await writer.WriteAsync($"{fieldInfo.Name} ={await fieldInfo.GetValueData(values[index1])}");
            }
            await writer.WriteAsync("}");
            return writer.ToString();
        }
    }
}