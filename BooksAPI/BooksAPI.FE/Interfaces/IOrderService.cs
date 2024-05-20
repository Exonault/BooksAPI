using BooksAPI.FE.Contracts.Order;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponse>> GetUserOrders(string token, string refreshToken, string userId);
    Task<OrderResponse> GetOrder(int id, string token, string refreshToken, string userId);
    Task<OrderModel> GetOrderModel(int id, string token, string refreshToken,  string userId);
    Task<bool> CreateOrder(OrderModel model, string token, string refreshToken, string userId);
    Task<bool> UpdateOrder(int id, OrderModel model, string token, string refreshToken, string userId);
    Task<bool> DeleteOrder(int id, string token, string refreshToken, string userId);
}