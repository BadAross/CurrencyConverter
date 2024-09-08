using CurrencyConverter.Models;

namespace CurrencyConverter.Operations.Interfaces;

public interface IMoneyOperations
{
    Task<Money> AddAsync(Money deposit, Money monetaryTransaction);
    
    Task<Money> SubtractAsync(Money deposit, Money monetaryTransaction);

    void SetCurrencyRates(List<CurrencyRate> currencyRates);
}