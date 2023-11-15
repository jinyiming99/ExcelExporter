using NPOI.SS.UserModel;

namespace ConsoleApp1;

public class FieldCellInfo
{
    public CellData Des;
    public CellData Name;
    public CellData Type;
    public CellData Use;
    public CellData Key;


}
public class FieldInfo
{
    public string FieldName;
    public string Type;
    
    public string TypeName;
    public FieldType FieldType;
}

public class ExcelFieldData
{
    public FieldCellInfo _info;
    
    public Dictionary<int,CellData> _datas;
    public ExcelFieldData(FieldCellInfo? info,Dictionary<int,CellData> datas)
    {
        _info = info;
        _datas = datas;
    }
}