using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Entities;

[Index(nameof(Id))]
public class Order
{
    public int Id { get; set; }
    
    public DateOnly Date { get; set; }

    public string Status { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    public string Place { get; set; } = string.Empty;
    
    public decimal Amount { get; set; }
    
    public int NumberOfItems { get; set; }
    
    public User User { get; set; }
    
}