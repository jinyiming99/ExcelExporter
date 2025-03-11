using ConsoleApp1.NewDatas;

namespace ConsoleApp1;

public static class ExportToFileHelper
{
    /// <summary>
    /// 导出数据结构
    /// </summary>
    /// <param name="outPath"></param>
    /// <param name="spaceName"></param>
    /// <param name="data"></param>
    public async static Task ExportStructFile(string outPath,string spaceName,ClassData data)
    {
        if (data._Datas.Count == 0)
            return;
        string filePath = outPath + "/" + data._name.ToUpperFirst() + ".cs";
        var steam = System.IO.File.CreateText(filePath);
        steam.WriteLineAsync($"namespace {spaceName}");
        steam.WriteLineAsync("{");

        
        await steam.WriteLineAsync($"   public class {data._name.ToUpperFirst()}Config");
        await steam.WriteLineAsync("    {");


        ///保存字段
        foreach (var field in data._Datas)
        {
            string str = await ExportDataFile(field.Value); 
            await steam.WriteLineAsync($"       {str}");
        }
        foreach (var field in data._Datas)
        {
            string str = await field.Value.ClassStr();
            if (string.IsNullOrEmpty(str))
                continue;
            await steam.WriteLineAsync($"    {str}");
        }
        await steam.WriteLineAsync("}");
        await steam.WriteLineAsync("}");
        await steam.FlushAsync();
        steam.Close();
        ;
    }
    /// <summary>
    /// 导出数据文件
    /// </summary>
    /// <param name="outPath"></param>
    /// <param name="spaceName"></param>
    /// <param name="data"></param>
    public async static Task ExportDataFile(string outPath, string spaceName,ClassData data)
    {
        if (data._Datas.Count == 0)
            return;
        string filePath = outPath + "/" + data._name.ToUpperFirst() + "Data.cs";
        var steam = System.IO.File.CreateText(filePath);
        await steam.WriteLineAsync($"namespace {spaceName}");
        await steam.WriteLineAsync("{");
        await steam.WriteLineAsync($"   public class {data._name.ToUpperFirst()}ConfigData");
        await steam.WriteLineAsync("    {");
        var typeName = FieldSturctValue.GetTypeString(data._key._value.DataType);
        await steam.WriteLineAsync($"       private readonly Dictionary<{typeName},{data._name.ToUpperFirst()}Config> dic = new()");
        await steam.WriteLineAsync("        {");
        foreach (var field in data._key._value._CellDatas)
        {
            if (field.Value == null)
                continue;
            await steam.WriteAsync("            {");
            await steam.WriteAsync($"{await data._key._value.GetValueData(field.Value.Txt)}" +","+ $" new {data._name.ToUpperFirst()}Config()");
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
        await steam.WriteLineAsync("        };");

        await steam.WriteLineAsync($"       public {data._name.ToUpperFirst()}Config Get({typeName} key)");
        await steam.WriteLineAsync("        {");
        await steam.WriteLineAsync("            if (dic.TryGetValue(key, out var value))");
        await steam.WriteLineAsync("            {");
        await steam.WriteLineAsync("                return value;");
        await steam.WriteLineAsync("            }");
        await steam.WriteLineAsync("            return null;");
        await steam.WriteLineAsync("        }");

        await steam.WriteLineAsync($"       public List<{data._name.ToUpperFirst()}Config> GetAll()");
        await steam.WriteLineAsync("        {");
        await steam.WriteLineAsync("            return dic.Values.ToList();");
        await steam.WriteLineAsync("        }");
        
        
        await steam.WriteLineAsync("    }");
        await steam.WriteLineAsync("}");
        await steam.FlushAsync();
        steam.Close();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public async static Task<string> ExportDataFile(FieldData data)
    {
        if (data._value == null)
        {
            Console.WriteLine($"field is null {data._name} {data._des}");
            return string.Empty;
        }
        return await data._value.SturctStr();
    }

    /// <summary>
    /// 导出所有配置文件
    /// </summary>
    /// <param name="outPath"></param>
    /// <param name="spaceName"></param>
    /// <param name="className"></param>
    public async static Task ExportAllConfigFile(string outPath, string spaceName, List<string> className)
    {
        if (className.Count == 0)
            return;
        string filePath = outPath + "/" + "ConfigDatas.cs";
        var steam = System.IO.File.CreateText(filePath);
        await steam.WriteLineAsync($"namespace {spaceName}");
        await steam.WriteLineAsync("{");
        await steam.WriteLineAsync($"   public class GameConfig");
        await steam.WriteLineAsync("    {");
        foreach (var name in className)
        {
            await steam.WriteLineAsync($"       public static {name.ToUpperFirst()}ConfigData {name}ConfigData = null;");
        }
        
        await steam.WriteLineAsync($"       public static void Init()");
        await steam.WriteLineAsync("       {");
        foreach (var name in className)
        {
            await steam.WriteLineAsync($"           {name}ConfigData = new {name.ToUpperFirst()}ConfigData();");
        }
        await steam.WriteLineAsync("       }");
        await steam.WriteLineAsync("    }");
        await steam.WriteLineAsync("}");
        await steam.FlushAsync();
        steam.Close();
    }
}