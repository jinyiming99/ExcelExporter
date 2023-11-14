// See https://aka.ms/new-console-template for more information

using System.IO;
using ConsoleApp1;

string dir = string.Empty;
if (args.Length > 0)
    dir = args[1];
else
    dir = Directory.GetCurrentDirectory();

Console.WriteLine($"dir = {dir}");
string[] files = Directory.GetFiles(dir);
files = files.Where(str =>Path.GetExtension(str).EndsWith(".xlsx")).ToArray();


foreach (string file in files)
{
    ExcelFile excelFile = new ExcelFile();
    excelFile.LoadFile(file);
    excelFile.LoadClass();
}

ExportData.Instance.Check();


Console.WriteLine("work done!");
