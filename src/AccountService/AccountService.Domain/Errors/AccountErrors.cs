using AccountService.Domain.Common;

namespace AccountService.Domain.Errors;

public static class AccountErrors
{
    public static readonly Error EmptyAccountNumber = new("EmptyAccountNumber", "Account number is empty.");
    public static readonly Error InvalidAccountNumberLength = new("InvalidAccountNumber", "Length account number != 20.");
    public static readonly Error InvalidAccountNumberFormat = new("InvalidAccountNumberFormat", "Account number doesn't contain digit.");
    public static readonly Error NegativeBalance = new("Account.NegativeBalance", "Balance cannot be negative.");
    public static readonly Error InsufficientFunds = new("Account.InsufficientFunds", "There are insufficient funds on the balance.");
    public static readonly Error DifferentCurrencies = new("Account.DifferentCurrencies", "Currencies do not match.");
    public static readonly Error EmptyUserId = new("InvalidUserId", "User ID is invalid.");
    public static readonly Error AccountClosed = new("AccountClosed", "Account is closed.");
    public static readonly Error AccountFrozen = new("AccountFrozen", "Account is frozen.");
    public static readonly Error AccountOpened = new("AccountOpened", "Account is opened.");
    public static readonly Error AccountHasBalance = new("AccountHasBalance", "Balance should be 0.");
}