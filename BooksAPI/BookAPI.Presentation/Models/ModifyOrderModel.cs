using System.ComponentModel.DataAnnotations;

namespace BookAPI.Presentation.Models;

public class ModifyOrderModel
{
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string Place { get; set; } = string.Empty;
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    public int NumberOfItems { get; set; }
}