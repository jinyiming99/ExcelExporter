using NPOI.SS.UserModel;

namespace ConsoleApp1;

public class ExportHelper
{
    public static FieldType GetType(CellData data)
    {
        switch (data.Txt.ToLower())
        {
            case "int":
            case "int32":
                return FieldType.Int;
            case "short":
                return FieldType.Short;
            case "float":
                return FieldType.Float;
            case "string":
                return FieldType.String;
            case "bool":
                return FieldType.Bool;
            case "list":
                return FieldType.List;
            case "dictionary":
                return FieldType.Dictionary;
            case "enum":
                return FieldType.Enum;
            case "array":
                return FieldType.Array;
            default:
            {
                var b = true;
                return b ? FieldType.Class : FieldType.Unkown;
            }
        }
    }

    public static List<CellData> GetColFromSheet(ISheet sheet,int index)
    {
        List<CellData> datas = new List<CellData>();
        for (int i = sheet.FirstRowNum; i < sheet.LastRowNum; i++)
        {
            var cell = sheet.GetRow(i).GetCell(index);
            if (cell == null )
                datas.Add(null);
            else
                datas.Add(new CellData(cell.ToString()));
        }

        return datas;
    }
}