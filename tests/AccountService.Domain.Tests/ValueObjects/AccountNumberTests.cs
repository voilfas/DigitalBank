using AccountService.Domain.Errors;
using AccountService.Domain.ValueObjects;
using FluentAssertions;

namespace AccountService.Domain.Tests.ValueObjects;

public class AccountNumberTests
{
    [Fact]
    public void Create_ShouldCreateAccountNumber_WhenValueIsValid()
    {
        string value = "12345678901234567890";
        var resultAccountNumber = AccountNumber.Create(value);
        
        resultAccountNumber.IsSuccess.Should().BeTrue();
        resultAccountNumber.Value.Should().NotBeNull();
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenValueIsEmpty()
    {
        string value = "";
        var resultAccountNumber = AccountNumber.Create(value);
        
        resultAccountNumber.IsFailure.Should().BeTrue();
        resultAccountNumber.Error.Should().Be(AccountErrors.EmptyAccountNumber);
    }
    
    [Fact]
    public void Create_ShouldBeFailure_WhenValueLengthIsInvalid()
    {
        string value = "1234567890";
        var resultAccountNumber = AccountNumber.Create(value);
        
        resultAccountNumber.IsFailure.Should().BeTrue();
        resultAccountNumber.Error.Should().Be(AccountErrors.InvalidAccountNumberLength);
    }
    
    [Fact]
    public void Create_ShouldBeFailure_WhenValueContainsNoDigits()
    {
        string value = "1234567890123456789s";
        var resultAccountNumber = AccountNumber.Create(value);
        
        resultAccountNumber.IsFailure.Should().BeTrue();
        resultAccountNumber.Error.Should().Be(AccountErrors.InvalidAccountNumberFormat);
    }
}