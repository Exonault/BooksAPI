using BooksAPI.BE.Constants;

namespace BooksAPI.BE.Messages;

public static class OrderMessages
{
    public const string DescriptionRequired = "Description is required and must not be empty";

    public const string StatusRequired = "Status is required and must not be empty";

    public static readonly string StatusValidationMessage = 
        $"Status must be one of the following: {string.Join(", ", OrderConstants.Status.OrderStatuses)}";

    public const string AmountRequired = "Amount is required and must not be empty.";

    public const string AmountValueMessage = "Amount must be with 2 digits after the decimal separator";

    public const string PlaceRequired = "Place is required and must be not empty";

    public const string NumberOfItemsRequired = "Number of items is required and must be greater than 0";

    public const string DateRequired = "Date is required and must not be empty";

    public const string DateValueMessage = "Date must be before today";


    public const string NoOrderWithId = "No order with id";

    public const string DeleteImpossible = "You can't delete this order";
}