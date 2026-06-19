using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.Errors;
using AccountService.Domain.Tests.Common;
using AccountService.Domain.ValueObjects;
using FluentAssertions;

namespace AccountService.Domain.Tests.Entities;

public class AccountTests
{
    [Fact]
    public void Create_ShouldCreateAccount_WhenValueIsValid()
    {
        var resultAccount = Account
            .Create(
                Guid.NewGuid(),
                AccountNumber
                    .Create("12345678901234567890")
                    .Value!,
                Currency.RUB);
        
        resultAccount.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenUserIdIsEmpty()
    {
        var resultAccount = Account
            .Create(
                Guid.Empty,
                AccountNumber
                    .Create("12345678901234567890")
                    .Value!,
                Currency.RUB);
        
        resultAccount.IsFailure.Should().BeTrue();
        resultAccount.Error.Should().Be(AccountErrors.EmptyUserId);
    }
    
    [Fact]
    public void Deposit_ShouldIncreaseBalance()
    {
        var account = AccountHelper.CreateAccount();

        account.Deposit(
            Money.Create(100, Currency.RUB).Value!);

        account.Balance.Amount.Should().Be(100);
    }
    
    [Fact]
    public void Withdraw_ShouldDecreaseBalance()
    {
        var account = AccountHelper.CreateAccount();

        account.Deposit(
            Money.Create(100, Currency.RUB).Value!);

        account.Withdraw(
            Money.Create(40, Currency.RUB).Value!);

        account.Balance.Amount.Should().Be(60);
    }
    
    [Fact]
    public void Freeze_ShouldChangeStatus()
    {
        var account = AccountHelper.CreateAccount();

        account.Freeze();

        account.Status.Should()
            .Be(AccountStatus.Frozen);
    }
    
    [Fact]
    public void Close_ShouldCloseAccount_WhenBalanceZero()
    {
        var account = AccountHelper.CreateAccount();

        var result = account.Close();

        result.IsSuccess.Should().BeTrue();

        account.Status.Should()
            .Be(AccountStatus.Closed);
    }

    [Fact]
    public void Unfreeze_ShouldReturnFailure_WhenStatusIsOpen()
    {
        var account = AccountHelper.CreateAccount();

        var result = account.Unfreeze();

        result.IsFailure.Should().BeTrue();
        account.Status.Should()
            .Be(AccountStatus.Open);
    }
    
    [Fact]
    public void Unfreeze_ShouldChangeStatus_WhenStatusIsFreeze()
    {
        var account = AccountHelper.CreateAccount();

        var resultFreeze = account.Freeze();
        
        var resultUnfreeze = account.Unfreeze();

        resultUnfreeze.IsSuccess.Should().BeTrue();
        account.Status.Should()
            .Be(AccountStatus.Open);
    }
}