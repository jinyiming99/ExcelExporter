namespace ConsoleApp1.Config;

public class ChangeValue<T> : ValueBase<T> 
{
    public delegate void ChangeHandler(T value);
    public event ChangeHandler OnChange;
    public T Value
    {
        get => _value;
        set
        {
            if (!_value.Equals(value))
            {
                _value = value;
            }
        }
    }
}