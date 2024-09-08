using CurrencyConverter.Converters.Interfaces;
using CurrencyConverter.Models;
using CurrencyConverter.Operations.Interfaces;

namespace CurrencyConverter.Operations.Implementations;

public sealed class MoneyOperations(IConverter converter) : IMoneyOperations
{
    private readonly IConverter _converter = converter;
    private List<CurrencyRate>? _currencyRates;

    public void SetCurrencyRates(List<CurrencyRate> currencyRates)
    {
        _currencyRates = currencyRates;
    }
    
    public async Task<Money> AddAsync(Money deposit, Money monetaryTransaction)
    {
        ChecksExchangeRatesAreSet();
        monetaryTransaction = await 
            TransactionConvertAsync(deposit.Currency, monetaryTransaction);

        var totalAmount = deposit.Amount + monetaryTransaction.Amount;

        return new Money(totalAmount, deposit.Currency);
    }

    public async Task<Money> SubtractAsync(Money deposit, Money monetaryTransaction)
    {
        ChecksExchangeRatesAreSet();
        monetaryTransaction = await 
            TransactionConvertAsync(deposit.Currency, monetaryTransaction);

        var totalAmount = deposit.Amount - monetaryTransaction.Amount;
        
        return new Money(totalAmount,  deposit.Currency);
    }

    private void ChecksExchangeRatesAreSet()
    {
        if (_currencyRates is null)
        {
            throw new InvalidOperationException("Не установлены валютные курсы");
        }
    }
    
    private async Task<Money> TransactionConvertAsync(Currency depositCurrency, Money monetaryTransaction)
        => await _converter.ConvertAsync(_currencyRates, monetaryTransaction, depositCurrency);
}