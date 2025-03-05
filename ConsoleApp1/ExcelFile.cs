using ConsoleApp1;
using ConsoleApp1.NewDatas;
using NPOI.XSSF.UserModel;
public class ExcelFile
{
     private string Path;
     private XSSFWorkbook Workbook;
     private XSSFFormulaEvaluator evaluator;
     public void LoadFile(string path)
     {
          Path = path;
          Workbook = new XSSFWorkbook(Path);
          evaluator = new XSSFFormulaEvaluator(Workbook);
     }

     public Dictionary<string,ExcelClassData> LoadClass()
     {
          Dictionary<string,ExcelClassData> compilationsData = new Dictionary<string, ExcelClassData>();
          for (int i = 0; i < Workbook.NumberOfSheets; i++)
          {
               ExcelClassData data = new ExcelClassData();
               var name = Workbook.GetSheetName(i);
               var classData = ClassLoader.LoadClass(Workbook.GetSheet(name),evaluator);
               compilationsData.Add(name,classData);
          }

          return compilationsData;
     }
     
}