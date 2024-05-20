using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Attribute;
using BooksAPI.FE.Messages;

namespace BooksAPI.FE.Model;

public class OrderModel
{
    [Required(ErrorMessage = OrderMessages.DateRequired)]
    [NotInFuture(ErrorMessage = OrderMessages.NotInFuture)]
    public DateTime? Date { get; set; } = DateTime.Today;

    [Required(ErrorMessage = OrderMessages.DescriptionRequired)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = OrderMessages.PlaceRequired)]
    public string Place { get; set; } = string.Empty;

    [Required(ErrorMessage = OrderMessages.AmountRequired)]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = OrderMessages.NumberOfItemsRequired)]
    public int NumberOfItems { get; set; }
}