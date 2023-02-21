namespace Lab1_piris.Data.Models;

public class Deposit
{
    public long Id { get; set; }

    public string ContractNumber { get; set; }

    public long TypeId { get; set; }

    public decimal Amount { get; set; }

    public DepositType Type { get; set; }

    public bool IsActive { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime NextInterestPayDate { get; set; }

    public long CurrencyId { get; set; }

    public Currency Currency { get; set; }

    public long OwnerId { get; set; }

    public Client Owner { get; set; }

    public long DepositAccountId { get; set; }

    public Account DepositAccount { get; set; }

    public long InterestAccountId { get; set; }

    public Account InterestAccount { get; set; }
}