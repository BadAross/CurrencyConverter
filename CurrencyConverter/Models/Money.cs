namespace CurrencyConverter.Models;

public sealed class Money(decimal amount, Currency currency)
{
    public decimal Amount { get; } = Math.Round(amount, 2);
    public Currency Currency { get; } = currency;

    public override string ToString()
    {
        return $"Счет: {Amount} {Currency.Code}";
    }
}