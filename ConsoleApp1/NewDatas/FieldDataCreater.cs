namespace ConsoleApp1.NewDatas;

public static class FieldDataCreater
{
    public static FieldData Creater(ExcelFieldData excelData,string className)
    {
        if (excelData == null || excelData._info == null || excelData._datas == null)
            return null;
        if (IsUse(excelData._info.Use))
        {
            FieldData data = new FieldData();
            data._client = IsClient(excelData._info.Use);
            data._server = IsServer(excelData._info.Use);
            data._isKey = IsKey(excelData._info.Key);
            data._className = className;
            data._name = excelData._info?.Name?.Txt;
            data._des = excelData._info.Des.Txt;
            data._value = CreateValue(excelData._info,excelData._datas,className);
            return data;
        }
        else
        {
            return null;
        }
    }

    private static BaseValue CreateValue(FieldCellInfo info,Dictionary<int, CellData> cellDatas,string className)
    {
        string type = info.Type.Txt.ToLower();
        BaseValue outData = null;
        Console.WriteLine($"type:{type}");
        Console.WriteLine($"info.des:{info.Des.Txt}");
        if (type.Contains("enum"))
        {
            var strs = type.Split(":");
            var name = strs.Where(str => !str.Contains("enum") && !string.IsNullOrEmpty(str)).ToArray();
            outData= new FieldEnumValue(name[0],info.Name.Txt,className);
        }
        else if (type.Contains("arr:") || type.Contains("json:"))
        {
            outData= new FieldArrayData(info.Type,info.Name.Txt,className);
        }
        else if (FieldSturctValue.IsStruct(info.Type))
        {
            var dType = FieldSturctValue.GetStruct(type);
            outData= new FieldSturctValue(dType,info.Name.Txt,className);
        }

        else
        {
            outData= null;
        }

        if (outData != null)
            outData._CellDatas = cellDatas;
        return outData;
    }

    /// <summary>
    /// 判断是否是关键字段
    /// </summary>
    /// <param name="Key"></param>
    /// <returns></returns>
    private static bool IsKey(CellData Key) => Key.Txt.ToLower().Equals("1");//Key.Txt.ToLower().Equals("unique");
    
    private static bool IsClient(CellData Use) => Use.Txt.ToLower().Contains("client") || Use.Txt.ToLower().Contains("all");

    private static bool IsServer(CellData Use)=> Use.Txt.ToLower().Contains("server")  || Use.Txt.ToLower().Contains("all");
    
    private static bool IsUse(CellData Use) => Use.Txt.ToLower().Contains("client") || Use.Txt.ToLower().Contains("server") || Use.Txt.ToLower().Contains("all");

 
}