namespace Lab1_piris.Data.Models;

public class DepositType
{
    public long Id { get; set; }

    public string Name { get; set; }

    public decimal Percent { get; set; }

    public bool IsRecallable { get; set; }
}