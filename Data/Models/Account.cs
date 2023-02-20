namespace Lab1_piris.Data.Models;

public class Account
{
    public long Id { get; set; }

    public long AccountTypeId { get; set; }

    public AccountType AccountType { get; set; }

    public string Number { get; set; }

    public long OwnerId { get; set; }

    public Client Owner { get; set; }

    public decimal Balance { get; set; }

    public bool IsOpen { get; set; }
}