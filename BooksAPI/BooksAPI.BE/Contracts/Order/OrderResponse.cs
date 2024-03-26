namespace BooksAPI.BE.Contracts.Order;

public class OrderResponse
{
    public DateOnly Date { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    public string Place { get; set; } = string.Empty;
    
    public decimal Amount { get; set; }
    
    public int NumberOfItems { get; set; }

    public string UserId { get; set; } = string.Empty;
}