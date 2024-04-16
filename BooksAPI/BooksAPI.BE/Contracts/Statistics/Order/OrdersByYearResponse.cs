namespace BooksAPI.BE.Contracts.Statistics.Order;

public class OrdersByYearResponse
{
    public int Year { get; set; }

    public int Items { get; set; }

    public decimal Price { get; set; }
}