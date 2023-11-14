using NPOI.SS.UserModel;

namespace ConsoleApp1;



public class ClassData
{
    private string _name;
    private List<FieldData> _fields = new List<FieldData>();

    
    public void LoadClass(ISheet sheet)
    {
        for (int i = sheet.FirstRowNum; i < sheet.LastRowNum; i++)
        {
            FieldData data = new FieldData();
            var list = ExportHelper.GetColFromSheet(sheet,i);
            if (data.LoadField(list))
                _fields.Add(data);
        }
    }
}