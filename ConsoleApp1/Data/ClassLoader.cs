using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ConsoleApp1.NewDatas;

public static class ClassLoader
{
    public static ExcelClassData LoadClass(ISheet sheet,XSSFFormulaEvaluator eva)
    {
        ExcelClassData outData = new();
        for (int i = 0; i < 255; i++)
        {
            var str = GetUse(sheet, i);
            if (string.IsNullOrEmpty(str))
                continue;
            var list = GetColFromSheet(sheet,eva,i);
            if (list.Count == 0)
                break;
            var info = FieldLoader.LoadField(list);
            
            if (info is not null)
            {
                var datas = FieldLoader.LoadDatas(list);
                ExcelFieldData data = new ExcelFieldData(info,datas);
                outData.AddField(data);
            }
        }

        return outData;
    }
    
    private static List<CellData> GetColFromSheet(ISheet sheet,XSSFFormulaEvaluator eva,int index)
    {
        List<CellData> datas = new List<CellData>();
        for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
        {
            var row = sheet.GetRow(i);
            if (row == null)
                continue;
            var cell = row.GetCell(index);
            if (cell == null )
                datas.Add(null);
            else
            {
                var str = cell.ToString();
                var v = eva.Evaluate(cell);
                if (v != null)
                {
                    switch (v.CellType)
                    {
                        case CellType.String:
                        {
                            datas.Add(new CellData(v.StringValue));
                            break;
                        }
                        case CellType.Numeric:
                        {
                            datas.Add(new CellData(v.NumberValue.ToString()));
                            break;
                        }
                        case CellType.Boolean:
                        {
                            datas.Add(new CellData(v.BooleanValue.ToString()));
                            break;
                        }
                        case CellType.Error:
                        {
                            Console.WriteLine($"error unknown type");
                            break;
                        }
                    }
                }
                else
                {
                    datas.Add(new CellData(str));
                }
            }
        }
        return datas;
    }

    private static string GetUse(ISheet sheet, int index)
    {
        if (sheet == null)
            return string.Empty;
        var row = sheet.GetRow((int)ConstDefine.UseLine);
        if (row == null)
            return string.Empty;
        var cell = row.GetCell(index);
        if (cell == null)
            return string.Empty;
        return cell.ToString();
    }
}