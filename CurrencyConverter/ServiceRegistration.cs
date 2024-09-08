using CurrencyConverter.Converters.Implementations;
using CurrencyConverter.Converters.Interfaces;
using CurrencyConverter.Operations.Implementations;
using CurrencyConverter.Operations.Interfaces;
using CurrencyConverter.Providers.Implementations;
using CurrencyConverter.Providers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter;

public static class ServiceRegistration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IConverter, Converter>();
        services.AddSingleton<IExchangeRateProvider, ExchangeRateProvider>();
        services.AddTransient<IMoneyOperations, MoneyOperations>();
    }
}