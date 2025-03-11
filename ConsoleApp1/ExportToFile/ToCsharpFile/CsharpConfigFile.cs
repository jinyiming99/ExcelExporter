using ConsoleApp1.Config;
using ConsoleApp1.NewDatas;

namespace ConsoleApp1;

public class CsharpConfigFile
{
    public static async Task Export(GlobalConfig config,Dictionary<string,ClassData> dic)
    {
        var tasks = new List<Task>();
        foreach (var v in dic)
        {
            Console.WriteLine($"export {v.Key}");
            tasks.Add(ExportToFileHelper.ExportStructFile(config._outPath,config._nameSpace,v.Value));
            tasks.Add(ExportToFileHelper.ExportDataFile(config._outPath,config._nameSpace,v.Value));
        }

        await Task.WhenAll(tasks);
        await ExportToFileHelper.ExportAllConfigFile(config._outPath,config._nameSpace,dic.Keys.ToList());
    }
}