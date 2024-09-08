using CurrencyConverter.Models;

namespace CurrencyConverter.Providers.Interfaces;

public interface IExchangeRateProvider
{
    Task<decimal> GetRateAsync(
        List<CurrencyRate> currencyRates,
        Currency fromCurrency, 
        Currency toCurrency);
}