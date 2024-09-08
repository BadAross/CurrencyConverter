# CurrencyConverter 
**CurrencyConverter** - библиотека, предназначенная для работы с финансами. Она позволяет производить финансовые транзакции с учетом конвертации валют.  
 
### Основная информация 
Библиотека реализует два метода работы с финансами: 
- Метод сложения 
- Метод вычитания 
 
Для работы с методами необходимо сформировать "курс валют" и передать его в библиотеку. 
 
#### Модели данных 
Реализовано три модели данных: 
1. **Currency** - модель валюты. Состоит из: 
   - Код валюты. 
    
2. **CurrencyRate** - курс валют. Состоит из: 
   - Валюта, из которой происходит конвертация. 
   - Валюта, в которую происходит конвертация. 
   - Курс для конвертации. 
    
3. **Money** - хранение денег. Состоит из: 
   - Количества валюты. 
   - Модели валюты. 
 
### Конвертация 
Конвертация валют происходит в классе `Converter`. Метод `ConvertAsync` проверяет необходимость конвертации (разная ли валюта), затем получает курс и умножает курс на количество валюты. 
 
### Получение курса 
Получение курса осуществляется через `ExchangeRateProvider`. Метод `GetRateAsync` проверяет наличие прямого курса и возвращает его. Если прямой курс не найден, производится поиск кросс-курса через третью валюту, и курсы перемножаются. 
 
### Взаимодействие 
Для работы с библиотекой необходимо выполнить следующие шаги: 
 
1. **Зарегистрировать сервис:**
``` C#
using CurrencyConverter;
using CurrencyConverter.Operations.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
ServiceRegistration.RegisterServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();
var operations = serviceProvider.GetService<IMoneyOperations>();
```
2.  **Создать список курсов валют и передать его в библиотеку:**
``` C#
using CurrencyConverter.Models;

var usd = new Currency("USD");
var eur = new Currency("EUR");
var rub = new Currency("RUB");

var currencyRates = new List<CurrencyRate>
{
    new CurrencyRate(usd, eur, 0.85m); 
    new CurrencyRate(eur, rub, 90.0m); 
    new CurrencyRate(rub, gbp, 0.011m);
    new CurrencyRate(eur, usd, 1 / 0.85m); 
    new CurrencyRate(rub, eur, 1 / 90.0m); 
    new CurrencyRate(gbp, rub, 1 / 0.011m);
};

operations.SetCurrencyRates(currencyRates);
```
3.  **Вызвать необходимую операцию:**
``` C#
using CurrencyConverter.Models;

var myMoney = new Money(10.0m, usd);
var moneyInUsd = new Money(100.0m, usd);

var addMoney = await operations.AddAsync(myMoney, moneyInUsd);
var subtractMoney = await operations.SubtractAsync(myMoney, moneyInUsd);
```
4. **Обработать исключение, если курс не будет найден:**
``` C#
try
{
    var result = await operations.AddAsync(myMoney, moneyInUsd);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
```