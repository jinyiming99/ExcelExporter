using ConsoleApp1;
using ConsoleApp1.NewDatas;
using GameFrameWork.DebugTools;
using NPOI.XSSF.UserModel;
public class ExcelFile
{
     private string Path;
     private XSSFWorkbook Workbook;

     public void LoadFile(string path)
     {
          Path = path;
          Workbook = new XSSFWorkbook(Path);
     }

     public Dictionary<string,ExcelClassData> LoadClass()
     {
          Dictionary<string,ExcelClassData> compilationsData = new Dictionary<string, ExcelClassData>();
          for (int i = 0; i < Workbook.NumberOfSheets; i++)
          {
               ExcelClassData data = new ExcelClassData();
               var name = Workbook.GetSheetName(i);
               DebugHelper.Log($"sheet name = {name}");
               var isheet = Workbook.GetSheet(name);
               if (isheet == null)
                    continue;
               var classData = ClassLoader.LoadClass(isheet);
               compilationsData.Add(name,classData);
          }

          return compilationsData;
     }
     
}