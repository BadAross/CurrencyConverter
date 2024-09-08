using CurrencyConverter.Models;

namespace CurrencyConverter.Converters.Interfaces;

public interface IConverter
{
    Task<Money?> ConvertAsync(List<CurrencyRate> currencyRates, 
        Money monetaryTransaction, Currency depositCurrency);
}