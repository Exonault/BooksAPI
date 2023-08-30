namespace BookAPI.Presentation.Models;

public class OrderListElementModel
{
    public Guid Id { get; set; }

    public DateOnly Date { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Place { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public int NumberOfItems { get; set; }
}