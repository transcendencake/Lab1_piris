namespace Lab1_piris.Data.Models;

public class Transaction
{
    public long Id { get; set; }

    public long AccountId { get; set; }

    public Account Account { get; set; }

    public decimal Amount { get; set; }

    public bool IsCommitted { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}