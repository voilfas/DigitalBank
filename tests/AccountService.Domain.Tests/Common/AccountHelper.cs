using AccountService.Domain.Entities;
using AccountService.Domain.Enums;
using AccountService.Domain.ValueObjects;

namespace AccountService.Domain.Tests.Common;

public class AccountHelper
{
    public static Account CreateAccount()
    {
        var accountNumber = AccountNumber
            .Create("12345678901234567890").Value!;

        return Account.
            Create(
            Guid.NewGuid(),
            accountNumber,
            Currency.RUB).Value!;
    }
}