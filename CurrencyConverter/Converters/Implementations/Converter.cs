using CurrencyConverter.Converters.Interfaces;
using CurrencyConverter.Models;
using CurrencyConverter.Providers.Interfaces;

namespace CurrencyConverter.Converters.Implementations;

public sealed class Converter(
    IExchangeRateProvider exchangeRateProvider) : IConverter
{
    private readonly IExchangeRateProvider _exchangeRateProvider = exchangeRateProvider;

    public async Task<Money?> ConvertAsync(List<CurrencyRate> currencyRates, 
        Money monetaryTransaction, Currency depositCurrency)
    {
        if (depositCurrency.Equals(monetaryTransaction.Currency))
        {
            return monetaryTransaction;
        }
        
        var rate = await _exchangeRateProvider.GetRateAsync(
            currencyRates,monetaryTransaction.Currency, depositCurrency);
        
        var rateAmount = monetaryTransaction.Amount * rate;
        return new Money(rateAmount, depositCurrency);
    }
}