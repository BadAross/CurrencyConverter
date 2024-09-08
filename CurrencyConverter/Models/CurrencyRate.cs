namespace CurrencyConverter.Models;

public sealed class CurrencyRate(Currency from, Currency to, decimal rate)
{
    public Currency From { get; } = from;
    public Currency To { get; } = to;
    public decimal Rate { get; } = rate;
}