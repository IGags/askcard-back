namespace Core.DbCustomTypes;

public class NormalizedString
{
    public string Value { get; }

    public NormalizedString(string value)
    {
        Value = value.ToLower();
    }

    public static implicit operator NormalizedString(string value)
    {
        return new NormalizedString(value);
    }

    public static implicit operator string(NormalizedString value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public override bool Equals(object obj)
    {
        if (obj is not NormalizedString str)
        {
            return false;
        }

        return Value.Equals(str.Value);
    }
}