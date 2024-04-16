namespace BooksAPI.BE.Contracts.Statistics.Order;

public class OrdersForMonthByYearResponse
{
    public int Month { get; set; }

    public int Items { get; set; }

    public decimal Price { get; set; }
}