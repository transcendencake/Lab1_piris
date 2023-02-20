namespace Lab1_piris.Data.Models;

public class AccountType
{
    public long Id { get; set; }

    public AccountTypeEnum AccountTypeEnum { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
}