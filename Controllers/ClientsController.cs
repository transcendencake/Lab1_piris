using Lab1_piris.ClientModels;
using Lab1_piris.Controllers.Services;
using Lab1_piris.Data;
using Lab1_piris.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ILogger<ClientsController> logger;
    private readonly ApplicationDbContext dbContext;
    private readonly TransactionService transactionService;

    public ClientsController(ILogger<ClientsController> logger, ApplicationDbContext dbContext, TransactionService transactionService)
    {
        this.logger = logger;
        this.dbContext = dbContext;
        this.transactionService = transactionService;
    }

    [HttpGet]
    public async Task<List<ClientModel>> Get()
    {
        var result = await GetClientsFromDb()
            .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                    .ThenBy(p => p.MiddleName)
            .ToListAsync();
        return result;
    }

    [HttpGet("{clientId:long}")]
    public async Task<ClientModel> GetClientById([FromRoute] long clientId)
    {
        return await GetClientsFromDb()
            .FirstAsync(p => p.Id == clientId);
    }

    [HttpPut("{clientId:long}")]
    public async Task<IActionResult> CreateOrUpdateClient([FromRoute] long clientId, [FromBody] ClientModel model)
    {
        var client = await dbContext.Clients.FirstOrDefaultAsync(p => p.Id == clientId);
        if (client == null)
        {
            client = new Client();
            dbContext.Clients.Add(client);
        }

        client.FirstName = model.FirstName;
        client.LastName = model.LastName;
        client.MiddleName = model.MiddleName;
        client.BirthDate = model.BirthDate;
        client.IsMale = model.IsMale;
        client.PassportSeries = model.PassportSeries;
        client.PassportNumber = model.PassportNumber;
        client.PassportIssuedBy = model.PassportIssuedBy;
        client.PassportIssuedAt = model.PassportIssuedAt;
        client.PassportId = model.PassportId;
        client.BirthPlace = model.BirthPlace;
        client.LivingCityId = model.LivingCity.Id;
        client.LivingAddress = model.LivingAddress;
        client.HomePhone = model.HomePhone;
        client.MobilePhone = model.MobilePhone;
        client.Email = model.Email;
        client.PlaceOfWork = model.PlaceOfWork;
        client.WorkingPosition = model.WorkingPosition;
        client.RegistrationCityId = model.RegistrationCity.Id;
        client.FamilyStateId = model.FamilyState.Id;
        client.CitizenshipId = model.Citizenship.Id;
        client.DisabilityId = model.Disability.Id;
        client.Pensioner = model.Pensioner;
        client.MonthIncome = model.MonthIncome;

        await dbContext.SaveChangesAsync();
        return Ok(client.Id);
    }

    [HttpPost("{clientId:long}/delete")]
    public async Task<IActionResult> DeleteClient([FromRoute] long clientId)
    {
        var client = await dbContext.Clients.FirstAsync(p => p.Id == clientId);
        dbContext.Clients.Remove(client);
        await dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("{clientId:long}/deposit")]
    public async Task<IEnumerable<SelectedItemModel>> GetDeposits([FromRoute] long clientId)
    {
        return await dbContext.Deposits
            .Where(p => p.OwnerId == clientId)
            .Select(p => new SelectedItemModel
            {
                Id = p.Id,
                Name = p.Owner.LastName + " " + p.Id
            })
            .ToListAsync();
    }

    [HttpGet("{clientId:long}/deposit/{depositId:long}")]
    public async Task<DepositModel> GetDeposit([FromRoute] long clientId, [FromRoute] long depositId)
    {
        return await dbContext.Deposits
            .Where(p => p.OwnerId == clientId)
            .Select(p => new DepositModel
            {
                Id = p.Id,
                Amount = p.Amount,
                ContractNumber = p.ContractNumber,
                CurrencyType = new SelectedItemModel
                {
                    Id = p.Currency.Id,
                    Name = p.Currency.Name
                },
                NextInterestPayDate = p.NextInterestPayDate,
                EndDate = p.EndDate,
                StartDate = p.StartDate,
                IsActive = p.IsActive,
                DepositType = new DepositTypeModel
                {
                    Id = p.Type.Id,
                    Name = p.Type.Name,
                    Percent = p.Type.Percent,
                    IsRecallable = p.Type.IsRecallable
                },
                DepositAccount = new SelectedItemModel
                {
                    Id = p.DepositAccount.Id,
                    Name = p.DepositAccount.Number
                },
                InterestAccount = new SelectedItemModel
                {
                    Id = p.InterestAccount.Id,
                    Name = p.InterestAccount.Number
                }
            })
            .FirstAsync(p => p.Id == depositId);
    }

    [HttpPost("{clientId:long}/deposit/{depositId:long}/pay-interest")]
    public async Task PayInterest([FromRoute] long clientId, [FromRoute] long depositId)
    {
        var deposit = await dbContext.Deposits
            .Include(p => p.Type)
            .Include(p => p.InterestAccount)
            .Include(p => p.DepositAccount)
            .FirstAsync(p => p.Id == depositId);
        var addedAmount = deposit.DepositAccount.Balance * deposit.Type.Percent / 100;
        var bankAccount = dbContext.Accounts
            .FirstAsync(p => p.AccountType.AccountTypeEnum == AccountTypeEnum.BankDevelopmentFund);
        transactionService.CreateTransaction(dbContext, bankAccount.Id, deposit.InterestAccount.Id, addedAmount);
        await dbContext.SaveChangesAsync();
    }

    [HttpPost("{clientId:long}/deposit/{depositId:long}/close")]
    public async Task CloseDeposit([FromRoute] long clientId, [FromRoute] long depositId)
    {
        var deposit = await dbContext.Deposits
            .Include(p => p.Type)
            .Include(p => p.InterestAccount)
            .Include(p => p.DepositAccount)
            .FirstAsync(p => p.Id == depositId);
        var cashboxAccount = await dbContext.Accounts
            .FirstAsync(p => p.AccountType.AccountTypeEnum == AccountTypeEnum.BankCashbox);
        var depositAmount = deposit.DepositAccount.Balance;
        var interestAmount = deposit.InterestAccount.Balance;
        transactionService.CreateTransaction(dbContext, deposit.InterestAccount.Id, cashboxAccount.Id, interestAmount);
        transactionService.CreateTransaction(dbContext, cashboxAccount.Id, -depositAmount);
        transactionService.CreateTransaction(dbContext, deposit.DepositAccount.Id, cashboxAccount.Id, depositAmount);
        transactionService.CreateTransaction(dbContext, cashboxAccount.Id, -interestAmount);
        deposit.IsActive = false;
        deposit.DepositAccount.IsOpen = false;
        deposit.InterestAccount.IsOpen = false;
        await dbContext.SaveChangesAsync();
    }

    [HttpPost("{clientId:long}/deposit")]
    public async Task<IActionResult> CreateDeposit([FromRoute] long clientId, [FromBody] DepositModel model)
    {
        var accountType = await dbContext.AccountTypes
            .FirstAsync(p => p.AccountTypeEnum == AccountTypeEnum.IndividualCurrent);
        var deposit = new Deposit
        {
            Amount = model.Amount,
            CurrencyId = model.CurrencyType.Id,
            ContractNumber = model.ContractNumber,
            NextInterestPayDate = model.StartDate.AddMonths(1),
            StartDate = model.StartDate,
            EndDate = model.EndDate ?? new DateTime(),
            OwnerId = clientId,
            TypeId = model.DepositType.Id,
            IsActive = true,
            DepositAccount = new Account
            {
                Balance = 0,
                Number = GenerateAcountNumber(),
                OwnerId = clientId,
                AccountTypeId = accountType.Id,
                IsOpen = true
            },
            InterestAccount = new Account
            {
                Balance = 0,
                Number = GenerateAcountNumber(),
                OwnerId = clientId,
                AccountTypeId = accountType.Id,
                IsOpen = true
            }
        };
        dbContext.Deposits.Add(deposit);
        await dbContext.SaveChangesAsync();

        var cashboxAccount = await dbContext.Accounts
            .FirstAsync(p => p.AccountType.AccountTypeEnum == AccountTypeEnum.BankCashbox);
        transactionService.CreateTransaction(dbContext, cashboxAccount.Id, deposit.Amount);
        transactionService.CreateTransaction(dbContext, cashboxAccount.Id, deposit.DepositAccountId, deposit.Amount);
        await dbContext.SaveChangesAsync();
        return Ok(deposit.Id);
    }

    private string GenerateAcountNumber()
    {
        var generator = new Random();
        var result = string.Empty;
        for (var i = 0; i < 13; i++)
        {
            result += generator.Next(0, 9);
        }

        return result;
    }

    private IQueryable<ClientModel> GetClientsFromDb()
    {
        return dbContext.Clients
            .Select(p => new ClientModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                MiddleName = p.MiddleName,
                BirthDate = p.BirthDate,
                IsMale = p.IsMale,
                PassportSeries = p.PassportSeries,
                PassportNumber = p.PassportNumber,
                PassportIssuedBy = p.PassportIssuedBy,
                PassportIssuedAt = p.PassportIssuedAt,
                PassportId = p.PassportId,
                BirthPlace = p.BirthPlace,
                LivingCity = new SelectedItemModel
                {
                    Id = p.LivingCity.Id,
                    Name = p.LivingCity.Name
                },
                LivingAddress = p.LivingAddress,
                HomePhone = p.HomePhone,
                MobilePhone = p.MobilePhone,
                Email = p.Email,
                PlaceOfWork = p.PlaceOfWork,
                WorkingPosition = p.WorkingPosition,
                RegistrationCity = new SelectedItemModel
                {
                    Id = p.RegistrationCity.Id,
                    Name = p.RegistrationCity.Name
                },
                FamilyState = new SelectedItemModel
                {
                    Id = p.FamilyState.Id,
                    Name = p.FamilyState.Name
                },
                Citizenship = new SelectedItemModel
                {
                    Id = p.Citizenship.Id,
                    Name = p.Citizenship.Name
                },
                Disability = new SelectedItemModel
                {
                    Id = p.Disability.Id,
                    Name = p.Disability.Name
                },
                Pensioner = p.Pensioner,
                MonthIncome = p.MonthIncome
            });
    }
}