namespace BookAPI.Presentation.Contracts.Requests.Order;

public class UpdateOrderRequest
{
    public DateOnly Date { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    public string Place { get; set; } = string.Empty;
    
    public decimal Amount { get; set; }
    
    public int NumberOfItems { get; set; }

}