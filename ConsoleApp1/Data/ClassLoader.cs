﻿using NPOI.SS.UserModel;

namespace ConsoleApp1.NewDatas;

public static class ClassLoader
{
    public static ExcelClassData LoadClass(ISheet sheet)
    {
        ExcelClassData outData = new();
        for (int i = 0; i < 255; i++)
        {
            
            var list = GetColFromSheet(sheet,i);
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
    
    private static List<CellData> GetColFromSheet(ISheet sheet,int index)
    {
        List<CellData> datas = new List<CellData>();
        for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
        {
            var cell = sheet.GetRow(i).GetCell(index);
            if (cell == null )
                datas.Add(null);
            else
                datas.Add(new CellData(cell.ToString()));
        }
        return datas;
    }
    
    private static void Check(ExcelClassData data)
    {
        if (data == null)
            return;
        
    }
}