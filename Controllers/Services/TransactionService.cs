using Lab1_piris.Data;
using Lab1_piris.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_piris.Controllers.Services;

public class TransactionService
{
    private readonly ApplicationDbContext _dbContext;
    public TransactionService(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public void CreateTransaction(ApplicationDbContext dbContext, long fromId, long toId, decimal amount)
    {
        dbContext.Transactions.Add(new Transaction
        {
            AccountId = fromId,
            Amount = -amount
        });
        dbContext.Transactions.Add(new Transaction
        {
            AccountId = toId,
            Amount = amount
        });
    }

    public void CreateTransaction(ApplicationDbContext dbContext, long toId, decimal amount)
    {
        _dbContext.Transactions.Add(new Transaction
        {
            AccountId = toId,
            Amount = amount
        });
    }

    public async Task CommitTransactions()
    {
        var transactions = await _dbContext.Transactions
            .Include(p => p.Account)
            .Where(p => !p.IsCommitted)
            .ToListAsync();
        foreach (var transaction in transactions)
        {
            transaction.Account.Balance += transaction.Amount;
            transaction.IsCommitted = true;
        }

        await _dbContext.SaveChangesAsync();
    }
}