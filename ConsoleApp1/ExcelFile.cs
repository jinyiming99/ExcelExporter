using ConsoleApp1;
using NPOI.XSSF.UserModel;
public class ExcelFile
{
     private string Path;
     private XSSFWorkbook Workbook;
     //private Dictionary<string,ClassData> classDatas = new Dictionary<string,ClassData>();
     public ExcelFile()
     {
          

     }

     public void LoadFile(string path)
     {
          Path = path;
          Workbook = new XSSFWorkbook(Path);
     }

     public void LoadClass()
     {
          for (int i = 0; i < Workbook.NumberOfSheets; i++)
          {
               ClassData data = new ClassData();
               var name = Workbook.GetSheetName(i);
               data.LoadClass(Workbook.GetSheet(name));
               ExportData.Instance.Add(name,data);
          }
     }
     
}