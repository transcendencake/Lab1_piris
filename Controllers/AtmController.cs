using Lab1_piris.ClientModels;
using Lab1_piris.Controllers.Services;
using Lab1_piris.Data;
using Lab1_piris.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1_piris.Controllers;

[ApiController]
[Route("[controller]")]
public class AtmController : ControllerBase
{
    public class AuthModel
    {
        public string AccountNumber { get; set; }
        public string Pin { get; set; }
    }

    public class WithdrawModel
    {
        public decimal Amount { get; set; }
    }

    private readonly ILogger<AtmController> logger;
    private readonly ApplicationDbContext dbContext;
    private readonly TransactionService transactionService;

    public AtmController(ILogger<AtmController> logger, ApplicationDbContext dbContext, TransactionService transactionService)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.transactionService = transactionService;
    }

    [HttpGet("{creditId:long}")]
    public async Task<decimal> GetBalance(long creditId)
    {
        return await dbContext.Credits
            .Where(p => p.Id == creditId)
            .Select(p => p.DepositAccount.Balance)
            .FirstAsync();
    }

    [HttpPost("{creditId:long}")]
    public async Task<IActionResult> Withdraw([FromRoute]long creditId, [FromBody]WithdrawModel model)
    {
        var credit = await dbContext.Credits
            .Include(p => p.Type)
            .Include(p => p.InterestAccount)
            .Include(p => p.DepositAccount)
            .FirstAsync(p => p.Id == creditId);
        if (model.Amount > credit.DepositAccount.Balance)
        {
            return BadRequest();
        }
        var cashboxAccount = await dbContext.Accounts
            .FirstAsync(p => p.AccountType.AccountTypeEnum == AccountTypeEnum.BankCashbox);
        transactionService.CreateTransaction(dbContext, credit.DepositAccount.Id, cashboxAccount.Id, model.Amount);
        transactionService.CreateTransaction(dbContext, cashboxAccount.Id, -model.Amount);
        await dbContext.SaveChangesAsync();
        await transactionService.CommitTransactions();
        return Ok();
    }

    [HttpPost("pin")]
    public async Task<IActionResult> Login([FromBody]AuthModel model)
    {
        var success = await dbContext.Accounts
            .Where(p => p.Number == model.AccountNumber)
            .Where(p => p.Owner.Pin == model.Pin)
            .AnyAsync();
        if (!success)
        {
            return Unauthorized();
        }

        var creditId = await dbContext.Credits
            .Where(p => p.DepositAccount.Number == model.AccountNumber)
            .Select(p => p.Id)
            .FirstAsync();
        return Ok(creditId);
    }
}
