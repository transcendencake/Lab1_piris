using Lab1_piris.Data;
using Lab1_piris.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1_piris.Controllers;

[ApiController]
[Route("[controller]")]
public class LookupsController : ControllerBase
{
    private readonly ILogger<LookupsController> logger;
    private readonly ApplicationDbContext dbContext;

    public LookupsController(ILogger<LookupsController> logger, ApplicationDbContext dbContext)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<LookupsModel> Get()
    {
        return new LookupsModel
        {
            Cities = await GetListLookups(dbContext.Cities),
            Citizenships = await GetListLookups(dbContext.Citizenship),
            Disabilities = await GetListLookups(dbContext.Disabilities),
            FamilyStates = await GetListLookups(dbContext.FamilyStates)
        };
    }

    private Task<List<SelectedItemModel>> GetListLookups<T>(DbSet<T> dbSet) where T : BaseListModel
    {
        return dbSet.Select(p => new SelectedItemModel
        {
            Id = p.Id,
            Name = p.Name
        }).ToListAsync();
    }
}