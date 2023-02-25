namespace Lab1_piris.ClientModels;

public class CreditModel
{
    public long Id { get; set; }
    public string ContractNumber { get; set; }
    public CreditTypeModel CreditType { get; set; }
    public SelectedItemModel CurrencyType { get; set; }
    public bool IsActive { get; set; }
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public long LengthInMonths { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? NextInterestPayDate { get; set; }
    public SelectedItemModel? DepositAccount { get; set; }
    public SelectedItemModel? InterestAccount { get; set; }
}