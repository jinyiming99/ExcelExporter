using NPOI.SS.UserModel;

namespace ConsoleApp1;

public class ExportHelper
{
    public static FieldType GetType(CellData data)
    {
        
            if (data.Txt.Equals("int") || data.Txt.Equals("int32"))
                return FieldType.Struct;
            else if (data.Txt.Equals("short") )
                return FieldType.Struct;
            else if (data.Txt.Equals("float") )
                return FieldType.Struct;
            else if (data.Txt.Equals("string") || data.Txt.Equals("str"))
                return FieldType.Struct;
            else if (data.Txt.Equals("bool") )
                return FieldType.Struct;
            else if (data.Txt.Contains("[]") )
                return FieldType.Array;
            else if (data.Txt.Contains("enum"))
                return FieldType.Enum;
            else
            {
                var b = true;//ExportHelper.FindClassName(data.Txt);
                return b ? FieldType.Class : FieldType.Unkown;
            }
        
    }
    
}