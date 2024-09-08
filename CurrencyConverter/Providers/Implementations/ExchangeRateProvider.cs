using CurrencyConverter.Models;
using CurrencyConverter.Providers.Interfaces;

namespace CurrencyConverter.Providers.Implementations;

public sealed class ExchangeRateProvider : IExchangeRateProvider
{
    public async Task<decimal> GetRateAsync(
        List<CurrencyRate> currencyRates, Currency fromCurrency, Currency toCurrency)
    {
        var directRate = await Task.Run(() 
            => GetDirectRate(currencyRates, fromCurrency, toCurrency));
        if (directRate is not null)
        {
            return directRate.Value;
        }
        
        var crossRate = await Task.Run(() 
            => GetCrossRate(currencyRates, fromCurrency, toCurrency));
        if (crossRate is not null)
        {
            return crossRate.Value;
        }
        
        throw new InvalidOperationException(
            $"Нет доступного курса для конвертации {fromCurrency.Code} в {toCurrency.Code}");
    }

    private static decimal? GetDirectRate(
        List<CurrencyRate> currencyRates, Currency fromCurrency, Currency toCurrency)
    {
        var directRate = currencyRates.FirstOrDefault(r =>
            r.From.Code == fromCurrency.Code && r.To.Code == toCurrency.Code);
        return directRate?.Rate;
    }

    private static decimal? GetCrossRate(
        List<CurrencyRate> currencyRates, Currency fromCurrency, Currency toCurrency)
    {
        foreach (var rate in currencyRates.Where(r =>
                     r.From.Code == fromCurrency.Code))
        {
            var crossRate = currencyRates.FirstOrDefault(r =>
                r.From.Code == rate.To.Code && r.To.Code == toCurrency.Code);
            if (crossRate != null)
            {
                return rate.Rate * crossRate.Rate;
            }
        }
        return null;
    }
}