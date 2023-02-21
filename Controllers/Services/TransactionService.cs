using Lab1_piris.Data;
using Lab1_piris.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_piris.Controllers.Services;

public class TransactionService
{
    private readonly ApplicationDbContext dbContext;
    public TransactionService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateTransaction(long fromId, long toId, decimal amount)
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

        await dbContext.SaveChangesAsync();
    }

    public async Task CreateTransaction(long toId, decimal amount)
    {
        dbContext.Transactions.Add(new Transaction
        {
            AccountId = toId,
            Amount = amount
        });

        await dbContext.SaveChangesAsync();
    }

    public async Task CommitTransactions()
    {
        var transactions = await dbContext.Transactions
            .Include(p => p.Account)
            .Where(p => !p.IsCommitted)
            .ToListAsync();
        foreach (var transaction in transactions)
        {
            transaction.Account.Balance += transaction.Amount;
            transaction.IsCommitted = true;
        }

        await dbContext.SaveChangesAsync();
    }
}