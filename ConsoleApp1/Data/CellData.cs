namespace ConsoleApp1;

public class CellData
{
    private string _txt;
    public string Txt => _txt;
    public CellData(string txt)
    {
        _txt = txt;
    }

    public static CellData Empty => new CellData(string.Empty);
    
}