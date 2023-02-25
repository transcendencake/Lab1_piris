using Lab1_piris.ClientModels;
using Lab1_piris.Data.Models;

public class CreditLookupsModel
{
    public IEnumerable<CreditTypeModel> CreditTypes { get; set; }
    public IEnumerable<SelectedItemModel> Currencies { get; set; }
}