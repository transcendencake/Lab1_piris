using Lab1_piris.ClientModels;
using Lab1_piris.Controllers.Services;
using Lab1_piris.Data;
using Lab1_piris.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1_piris.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> logger;
    private readonly ApplicationDbContext dbContext;
    private readonly TransactionService transactionService;

    public AccountsController(ILogger<AccountsController> logger, ApplicationDbContext dbContext, TransactionService transactionService)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IEnumerable<AccountModel>> Get()
    {
        return await dbContext.Accounts
            .Select(p => new AccountModel
            {
                Balance = p.Balance,
                Code = p.AccountType.Code,
                Number = p.Number,
                Name = p.AccountType.Name,
                IsActive = p.AccountType.IsActive,
                IsOpen = p.IsOpen
            })
            .ToListAsync();
    }

    [HttpPost("close-day")]
    public async Task CloseDay()
    {
        await transactionService.CommitTransactions();
    }
}