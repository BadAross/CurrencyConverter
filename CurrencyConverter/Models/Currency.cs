namespace CurrencyConverter.Models;

public sealed class Currency(string code)
{
    public string Code { get; } = code;

    public override bool Equals(object? obj)
    {
        if (obj is Currency other)
        {
            return Code == other.Code;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code);
    }
}