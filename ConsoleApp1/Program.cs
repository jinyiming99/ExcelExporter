// See https://aka.ms/new-console-template for more information

using System.IO;
using ConsoleApp1;
using ConsoleApp1.Config;
using ConsoleApp1.NewDatas;
using GameFrameWork.DebugTools;

string path = Directory.GetCurrentDirectory();
string dir = Path.Combine(path, "Config.txt");
var config = GlobalConfig.Create<GlobalConfig>(dir);
if (config == null)
{
    ConfigFile.CreateRaw<GlobalConfig>(dir);
    return;
}

DebugHelper.Log($"dir = {config._inPath}");
string[] files = Directory.GetFiles(config._inPath);
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
    await ExportToFileHelper.ExportStructFile(config._outPath,config._nameSpace,v.Value);
    await ExportToFileHelper.ExportDataFile(config._outPath,config._nameSpace,v.Value);
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


