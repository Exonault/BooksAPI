namespace BooksAPI.BE.Messages;

public static class OrderMessages
{
    public const string DescriptionMessage = "Description is required and must not be empty";

    public const string AmountMessage = "Amount is required and must not be empty.";

    public const string AmountValueMessage = "Amount must be with 2 digits after the decimal separator";

    public const string PlaceMessage = "Place is required and must be not empty";

    public const string NumberOfItemsMessage = "Number of items is required and must be greater than 0";

    public const string DateMessage = "Date is required and must not be empty";

    public const string DateValueMessage = "Date must be before today";


    public const string NoOrderWithId = "No order with id";

    public const string DeleteImpossible = "You can't delete this order";
}