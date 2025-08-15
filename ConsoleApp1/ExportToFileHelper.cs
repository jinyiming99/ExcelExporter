using ConsoleApp1.NewDatas;

namespace ConsoleApp1;

public static class ExportToFileHelper
{
    public async static Task ExportStructFile(string outPath,string spaceName,ClassData data)
    {
        if (data._Datas.Count == 0)
            return;
        string filePath = outPath + "/" + data._name + ".cs";
        var steam = System.IO.File.CreateText(filePath);
        await steam.WriteLineAsync($"using System.Collections.Generic;");
        await steam.WriteLineAsync($"using System;");
        
        steam.WriteLineAsync($"namespace {spaceName}");
        steam.WriteLineAsync("{");

        
        await steam.WriteLineAsync($"public class {data._name.ToUpperFirst()}");
        await steam.WriteLineAsync("{");
        foreach (var field in data._Datas)
        {
            string str = await field.Value.ClassStr();
            if (string.IsNullOrEmpty(str))
                continue;
            await steam.WriteLineAsync($"    {str}");
        }

        
        foreach (var field in data._Datas)
        {
            string str = await ExportDataFile(field.Value); 
            await steam.WriteLineAsync($"   {str}");
        }
        

        
        await steam.WriteLineAsync("}");
        await steam.WriteLineAsync("}");
        await steam.FlushAsync();
        steam.Close();
    }

    public async static Task ExportDataFile(string outPath, string spaceName,ClassData data)
    {
        if (data._Datas.Count == 0)
            return;
        string filePath = outPath + "/" + data._name.ToUpperFirst() + "Data.cs";
        var steam = System.IO.File.CreateText(filePath);
        await steam.WriteLineAsync($"using System.Collections.Generic;");
        await steam.WriteLineAsync($"namespace {spaceName}");
        await steam.WriteLineAsync("{");
        await steam.WriteLineAsync($"public class {data._name.ToUpperFirst()}Data");
        await steam.WriteLineAsync("{");
        var typeName = FieldSturctValue.GetTypeString(data._key._value.DataType);
        await steam.WriteLineAsync($"   private readonly Dictionary<{typeName},{data._name.ToUpperFirst()}> dic = new()");
        await steam.WriteLineAsync("   {");
        foreach (var field in data._key._value._CellDatas)
        {
            if (field.Value == null)
                continue;
            await steam.WriteAsync("      {");
            await steam.WriteAsync($"{await data._key._value.GetValueData(field.Value.Txt)}" +","+ $" new {data._name.ToUpperFirst()}()");
            await steam.WriteAsync("{");
            int index = 0;
            foreach (var f in data._Datas)
            {
                if (index++ != 0)
                    await steam.WriteAsync(",");
                var str = await f.Value.GetData(field.Key);
                await steam.WriteAsync($"{str}");
            }
            await steam.WriteAsync("}");
            await steam.WriteLineAsync("},");
        }
        await steam.WriteLineAsync("   };");

        await steam.WriteLineAsync($"   public {data._name.ToUpperFirst()} Get({typeName} key)");
        await steam.WriteLineAsync("   {");
        await steam.WriteLineAsync("       if (dic.TryGetValue(key, out var value))");
        await steam.WriteLineAsync("       {");
        await steam.WriteLineAsync("           return value;");
        await steam.WriteLineAsync("       }");
        await steam.WriteLineAsync("       return null;");
        await steam.WriteLineAsync("   }");

        await steam.WriteLineAsync($"   public List<{data._name.ToUpperFirst()}> GetAll()");
        await steam.WriteLineAsync("   {");
        await steam.WriteLineAsync("       return dic.Values.ToList();");
        await steam.WriteLineAsync("   }");
        
        
        await steam.WriteLineAsync("}");
        await steam.WriteLineAsync("}");
        await steam.FlushAsync();
        steam.Close();
    }

    public async static Task<string> ExportDataFile(FieldData data)
    {
        if (data._value == null)
        {
            Console.WriteLine($"field is null {data._des}");
            return string.Empty;
        }
        return await data._value.SturctStr();
    }
}