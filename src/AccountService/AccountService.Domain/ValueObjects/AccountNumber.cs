using AccountService.Domain.Common;
using AccountService.Domain.Errors;

namespace AccountService.Domain.ValueObjects;

public record AccountNumber
{
    public string Value { get; }

    private AccountNumber(string value)
    {
        Value = value;
    }

    public static Result<AccountNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<AccountNumber>.Failure(AccountErrors.EmptyAccountNumber());

        if (value.Length != 20)
            return Result<AccountNumber>.Failure(AccountErrors.InvalidAccountNumber());

        if (!value.All(char.IsDigit))
            return Result<AccountNumber>.Failure(AccountErrors.InvalidAccountNumberFormat());

        return Result<AccountNumber>.Success(new AccountNumber(value));
    }

    public override string ToString()
    {
        return Value;
    }
}