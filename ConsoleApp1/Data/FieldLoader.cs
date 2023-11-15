using NPOI.SS.UserModel;

namespace ConsoleApp1.NewDatas;

public static class FieldLoader
{
    public static FieldCellInfo? LoadField(List<CellData> row)
    {
        FieldCellInfo data = new FieldCellInfo();
        var cell = row[(int)ConstDefine.UseLine];
        if (cell == null)
            return null;
        data.Des = row[(int)ConstDefine.DesLine];
        data.Name = row[(int)ConstDefine.NameLine];
        data.Type = row[(int)ConstDefine.TypeLine];
        data.Use = cell;
        cell = row[(int)ConstDefine.KeyLine];
        data.Key = cell == null ? CellData.Empty : cell;
        return data;
    }

    public static Dictionary<int, CellData> LoadDatas(List<CellData> row)
    {
        Dictionary<int, CellData> outData = new();

        for (int i = (int)ConstDefine.DataLine; i < row.Count; i++)
        {
            outData.Add(i, row[i]);
        }
        
        return outData;
    }
    

}