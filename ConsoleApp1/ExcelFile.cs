using ConsoleApp1;
using ConsoleApp1.NewDatas;
using NPOI.XSSF.UserModel;
public class ExcelFile
{
     private string Path;
     private string Name;
     private XSSFWorkbook Workbook;
     private XSSFFormulaEvaluator evaluator;
     public void LoadFile(string path)
     {
          Path = path;
          Name = System.IO.Path.GetFileNameWithoutExtension(path);
          Workbook = new XSSFWorkbook(Path);
          evaluator = new XSSFFormulaEvaluator(Workbook);
     }

     public Dictionary<string,ExcelClassData> LoadClass()
     {
          Dictionary<string,ExcelClassData> compilationsData = new Dictionary<string, ExcelClassData>();


          var name = Workbook.GetSheetName(0);
          var classData = ClassLoader.LoadClass(Workbook.GetSheet(name),evaluator);
          compilationsData.Add(Name,classData);
          return compilationsData;
     }
     
}