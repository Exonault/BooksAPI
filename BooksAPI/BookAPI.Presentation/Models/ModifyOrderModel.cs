using System.ComponentModel.DataAnnotations;

namespace BookAPI.Presentation.Models;

public class ModifyOrderModel
{
    [Required(ErrorMessage = "Date is required")]
    public DateOnly Date { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Place is required")]
    public string Place { get; set; } = string.Empty;

    [Required(ErrorMessage = "Amount is required")]
    [Range(1, double.MaxValue, ErrorMessage = "Price must be at least 1")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Number of items is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Number of items must be at least 1")]
    public int NumberOfItems { get; set; }
}