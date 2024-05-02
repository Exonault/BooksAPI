using BooksAPI.FE.Contracts.Order;
namespace BooksAPI.FE.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponse>> GetUserOrders(string token, string refreshToken, string userId);
}