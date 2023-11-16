// See https://aka.ms/new-console-template for more information

using System.IO;
using ConsoleApp1;
using ConsoleApp1.NewDatas;

string dir = string.Empty;
string outDir = string.Empty;
string nameSpace = string.Empty;
bool isServer = false;
bool isClient = false;

bool[] argsCheck = new bool[4];
if (args.Length > 0)
{

    foreach (var arg in args)
    {
        if (arg.Contains("--I="))
        {
            dir = arg.Replace("--I=", "");
            argsCheck[0] = true;
            continue;
        }

        if (arg.Contains("--O="))
        {
            outDir = arg.Replace("--O=", "");
            argsCheck[1] = true;
            continue;
        }

        if (arg.Contains("--P="))
        {
            var str = arg.Replace("--P=", "");
            if (str == "server")
            {
                isServer = true;
            }
            else if (str == "client")
            {
                isClient = true;
            }
            argsCheck[2] = true;
            continue;
        }

        if (arg.Contains("--N="))
        {
            nameSpace = arg.Replace("--N=", "");
            argsCheck[3] = true;
            continue;
        }
            
    }

}
if (!argsCheck[0])
    dir = Directory.GetCurrentDirectory();
if (!argsCheck[1])
    outDir = Directory.GetCurrentDirectory();
if (!argsCheck[2])
    isClient = true;
if (!argsCheck[3])
    nameSpace = "Config";

    

Console.WriteLine($"dir = {dir}");
string[] files = Directory.GetFiles(dir);
files = files.Where(str =>Path.GetExtension(str).EndsWith(".xlsx")).ToArray();

ExcelData excelData = new ();

foreach (string file in files)
{
    ExcelFile excelFile = new ExcelFile();
    excelFile.LoadFile(file);
    var data = excelFile.LoadClass();
    foreach (var v in data)
    {
        excelData.Add(v.Key,v.Value);
    }
}

var dic = Check(excelData);

foreach (var v in dic)
{
    await ExportToFileHelper.ExportStructFile(outDir,nameSpace,v.Value);
    await ExportToFileHelper.ExportDataFile(outDir,nameSpace,v.Value);
}


Console.WriteLine("work done!");


Dictionary<string,ClassData> Check(ExcelData data)
{
    Dictionary<string, ClassData> outDic = new();
    foreach (var classInfo in data._classData)
    {
        Console.WriteLine($"导表 {classInfo.Key}");
        ClassData classData = new();
        classData._name = classInfo.Key.Split("%")[0];
        foreach (var fieldInfo in classInfo.Value._fields)
        {
            var fieldData = FieldDataCreater.Creater(fieldInfo);
            if (fieldData == null)
            {
                Console.WriteLine($"field is null {fieldInfo._info.Des.Txt}");
                continue;
            }

            if (fieldData._isKey)
            {
                classData._key = fieldData;
            }
            classData._Datas.Add(fieldData._name,fieldData);
        }
        outDic.Add(classData._name,classData);
    }

    return outDic;
}


