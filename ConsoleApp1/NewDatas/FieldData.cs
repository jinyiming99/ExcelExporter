namespace ConsoleApp1.NewDatas;

public class FieldData
{
    public string _des;
    public string _name;
    public string _className;
    public bool _server;
    public bool _client;
    public bool _isKey;
    public BaseValue _value;


    public async Task<string> ClassStr()
    {
        return await _value.ClassStr();
    }
    
    public async Task<string> SturctStr()
    {
        return await _value.SturctStr();
    }

    public async Task<string> GetData(int index)
    {
        return await _value.GetData(index);
    }
}