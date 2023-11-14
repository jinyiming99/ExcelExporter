using NPOI.SS.UserModel;

namespace ConsoleApp1;


public class FieldData
{
    private FieldType _type;
    private string _name;
    private CellData _typeData;
    private bool _isUse;
    private List<CellData> _datas = new List<CellData>();
    public bool LoadField(List<CellData> row)
    {
        var cell = row[(int)ConstDefine.UseLine];
        if (cell == null)
            return false;
        _isUse = cell.Txt.Contains("c");
        if (!_isUse)
            return false;
        _name = row[(int)ConstDefine.NameLine].Txt;
        _typeData = new CellData(row[(int)ConstDefine.TypeLine].Txt);
        for (int i = (int)ConstDefine.DataLine; i < row.Count; i++)
        {
            _datas.Add(row[i]);
        }

        return true;
    }
    
    private string GetCell(IRow row,int index)
    {
        if (!_isUse)
            return String.Empty;
        if (row.RowNum < index)
            return string.Empty;
        return row.GetCell(index).ToString();
    }
}