namespace ConsoleApp1.NewDatas;

public static class FieldDataCreater
{
    public static FieldData Creater(ExcelFieldData excelData, string className)
    {
        if (excelData == null)
            return null;
        if (IsUse(excelData._info.Use))
        {
            FieldData data = new FieldData();
            data._client = IsClient(excelData._info.Use);
            data._server = IsServer(excelData._info.Use);
            data._isKey = IsKey(excelData._info.Key);
            data._name = excelData._info.Name.Txt;
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
        if (type.Contains("enum"))
        {
            var strs = type.Split(":");
            var name = strs.Where(str => !str.Contains("enum") && !string.IsNullOrEmpty(str)).ToArray();
            outData= new FieldEnumValue(name[0],info.Name.Txt);
        }
        else if (type.Contains("[]"))///判断是否是数组
        {
            outData= new FieldArrayData(info.Type,info.Name.Txt ,className);
        }
        else if (FieldSturctValue.IsStruct(info.Type))//判断是否是结构体
        {
            outData= new FieldSturctValue(FieldSturctValue.GetStruct(info.Type.Txt),info.Name.Txt);
        }

        else
        {
            outData= null;
        }

        if (outData != null)
            outData._CellDatas = cellDatas;
        return outData;
    }
    
    private static bool IsKey(CellData Key) => Key.Txt.ToLower().Equals("unique");
    
    private static bool IsClient(CellData Use) => Use.Txt.ToLower().Contains("c");

    private static bool IsServer(CellData Use)=> Use.Txt.ToLower().Contains("s");
    
    private static bool IsUse(CellData Use) => Use.Txt.ToLower().Contains("c") || Use.Txt.ToLower().Contains("s");

 
}