using AccountService.Domain.Enums;
using AccountService.Domain.ValueObjects;
using FluentAssertions;

namespace AccountService.Domain.Tests.ValueObjects;

public class AccountMoneyTests
{
    [Fact]
    public void Create_ShouldCreateMoney_WhenValueIsValid()
    {
        var resultMoney = Money.Create(100, Currency.RUB);
        
        resultMoney.IsSuccess.Should().BeTrue();
        resultMoney.Value!.Amount.Should().Be(100);
        resultMoney.Value.Currency.Should().Be(Currency.RUB);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenAmountIsNegative()
    {
        var resultMoney = Money.Create(-100, Currency.RUB);
        
        resultMoney.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Add_ShouldIncreaseAmount()
    {
        var money = Money.Create(100, Currency.RUB).Value;
        
        var anotherMoney = Money.Create(50, Currency.RUB).Value;
        
        var result = money!.Add(anotherMoney!);
        
        result.IsSuccess.Should().BeTrue();
        result.Value!.Amount.Should().Be(150);
    }

    [Fact]
    public void Add_ShouldReturnFailure_WhenCurrenciesAreDifferent()
    {
        var money = Money.Create(100, Currency.RUB).Value;
        
        var anotherMoney = Money.Create(50, Currency.EUR).Value;
        
        var result = money!.Add(anotherMoney!);
        
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Subtract_ShouldDecreaseAmount()
    {
        var money = Money.Create(100, Currency.RUB).Value;
        
        var anotherMoney = Money.Create(50, Currency.RUB).Value;
        
        var result = money!.Subtract(anotherMoney!);
        
        result.IsSuccess.Should().BeTrue();
        result.Value!.Amount.Should().Be(50);
    }

    [Fact]
    public void Subtract_ShouldReturnFailure_WhenCurrenciesAreDifferent()
    {
        var money = Money.Create(100, Currency.RUB).Value;
        
        var anotherMoney = Money.Create(50, Currency.EUR).Value;

        var result = money!.Subtract(anotherMoney!);
        
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public void Subtract_ShouldReturnFailure_WhenInsufficientFunds()
    {
        var money = Money.Create(100, Currency.RUB).Value;
        
        var anotherMoney = Money.Create(150, Currency.RUB).Value;

        var result = money!.Subtract(anotherMoney!);
        
        result.IsFailure.Should().BeTrue();
    }
}