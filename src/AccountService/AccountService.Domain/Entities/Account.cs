using AccountService.Domain.Common;
using AccountService.Domain.Enums;
using AccountService.Domain.Errors;
using AccountService.Domain.ValueObjects;

namespace AccountService.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public AccountNumber AccountNumber { get; private set; }
    public Money Balance { get; private set; }
    public AccountStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Currency Currency => Balance.Currency;

    private Account(Guid userId, AccountNumber accountNumber, Currency currency)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        AccountNumber = accountNumber;
        Balance = Money.Zero(currency);
        Status = AccountStatus.Open;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Result<Account> Create(Guid userId, AccountNumber accountNumber, Currency currency)
    {
        if (userId == Guid.Empty)
            return Result<Account>.Failure(AccountErrors.EmptyUserId);
        
        return Result<Account>.Success(new Account(userId, accountNumber, currency));
    }

    public Result Deposit(Money money)
    {
        var activeResult = CheckActive();
        if (activeResult.IsFailure)
            return Result.Failure(activeResult.Error);
        
        var resultBalance = Balance.Add(money);
        
        if (resultBalance.IsFailure)
            return Result.Failure(resultBalance.Error);
        
        Balance = resultBalance.Value!;
        
        UpdateTimestamp();
        
        return Result.Success();
    }

    public Result Withdraw(Money money)
    {
        var activeResult = CheckActive();
        if (activeResult.IsFailure)
            return Result.Failure(activeResult.Error);
        
        var resultBalance = Balance.Subtract(money);
        if (resultBalance.IsFailure)
            return Result.Failure(resultBalance.Error);
        
        Balance = resultBalance.Value!;
        
        UpdateTimestamp();
        
        return Result.Success();
    }

    public Result Freeze()
    {
        if (Status == AccountStatus.Closed)
            return Result.Failure(
                AccountErrors.AccountClosed);

        if (Status == AccountStatus.Frozen)
            return Result.Failure(
                AccountErrors.AccountFrozen);

        Status = AccountStatus.Frozen;

        UpdateTimestamp();

        return Result.Success();
    }

    public Result Unfreeze()
    {
        if (Status == AccountStatus.Closed)
            return Result.Failure(AccountErrors.AccountClosed);
        
        if (Status == AccountStatus.Open)
            return Result.Failure(AccountErrors.AccountOpened);
        
        Status = AccountStatus.Open;
        
        UpdateTimestamp();
        
        return Result.Success();
    }

    public Result Close()
    {
        if (Balance.Amount != decimal.Zero)
            return Result.Failure(AccountErrors.AccountHasBalance);

        if (Status == AccountStatus.Closed)
            return Result.Failure(AccountErrors.AccountClosed);
        
        Status = AccountStatus.Closed;
        
        UpdateTimestamp();
        
        return Result.Success();
    }

    private Result CheckActive()
    {
        if (Status == AccountStatus.Closed)
            return Result.Failure(
                AccountErrors.AccountClosed);

        if (Status == AccountStatus.Frozen)
            return Result.Failure(
                AccountErrors.AccountFrozen);

        return Result.Success();
    }
    
    private void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}