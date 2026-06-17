using AccountService.Domain.Common;
using AccountService.Domain.Enums;
using AccountService.Domain.Errors;

namespace AccountService.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }
    public static Money Zero(Currency currency) => new Money(0, currency);

    public static Result<Money> Create(decimal amount, Currency currency)
    {
        if (amount < 0)
            return Result<Money>.Failure(AccountErrors.NegativeBalance);
        
        return Result<Money>.Success(new Money(amount, currency));
    }
    
    public Result<Money> Add(Money anotherMoney)
    {
        var matchResult = IsMatch(anotherMoney);
        if (matchResult.IsFailure)
            return Result<Money>.Failure(matchResult.Error!);
        
        return Result<Money>.Success(new Money(Amount + anotherMoney.Amount, Currency));
    }

    public Result<Money> Subtract(Money anotherMoney)
    {
        var matchResult =  IsMatch(anotherMoney);
        if (matchResult.IsFailure)
            return Result<Money>.Failure(matchResult.Error!);
        
        if (Amount < anotherMoney.Amount)
            return Result<Money>.Failure(AccountErrors.InsufficientFunds);
        
        return Result<Money>.Success(new Money(Amount - anotherMoney.Amount, Currency));
    }

    private Result IsMatch(Money anotherMoney)
    {
        if (Currency != anotherMoney.Currency)
            return Result.Failure(AccountErrors.DifferentCurrencies);
        
        return Result.Success();
    }

    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }
}