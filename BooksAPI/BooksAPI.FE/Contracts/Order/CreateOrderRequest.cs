namespace BooksAPI.FE.Contracts.Order;

public class CreateOrderRequest
{
    public DateOnly Date { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Place { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public int NumberOfItems { get; set; }

    public string UserId { get; set; } = string.Empty;
}