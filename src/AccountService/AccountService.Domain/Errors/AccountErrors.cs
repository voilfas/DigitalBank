using AccountService.Domain.Common;

namespace AccountService.Domain.Errors;

public class AccountErrors
{
    public static Error EmptyOwnerName() => new Error("Account.EmptyOwnerName", "The owner name is required.");
    public static Error NegativeBalance() => new Error("Account.NegativeBalance", "Balance cannot be negative.");
}