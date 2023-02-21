using Lab1_piris.ClientModels;
using Lab1_piris.Data.Models;

public class DepositLookupsModel
{
    public IEnumerable<DepositTypeModel> DepositTypes { get; set; }
    public IEnumerable<SelectedItemModel> Currencies { get; set; }
}