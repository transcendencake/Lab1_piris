using Lab1_piris.ClientModels;
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

    public ClientsController(ILogger<ClientsController> logger, ApplicationDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public Task<List<ClientModel>> Get()
    {
        return GetClientsFromDb().ToListAsync();
    }

    [HttpGet("{clientId:long}")]
    public Task<ClientModel> GetSubcontractById([FromRoute] long clientId)
    {
        return GetClientsFromDb()
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

        client.Id = model.Id;
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
        client.СitizenshipId = model.Citizenship.Id;
        client.DisabilityId = model.Disability.Id;
        client.Pensioner = model.Pensioner;
        client.MonthIncome = model.MonthIncome;

        await dbContext.SaveChangesAsync();
        return Ok(client.Id);
    }

    [HttpDelete("{clientId:long}")]
    public async Task<IActionResult> DeleteClient([FromRoute] long clientId)
    {
        var client = await dbContext.Clients.FirstAsync(p => p.Id == clientId);
        dbContext.Clients.Remove(client);
        await dbContext.SaveChangesAsync();

        return Ok();
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