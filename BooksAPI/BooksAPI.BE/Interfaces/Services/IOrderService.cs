using BooksAPI.BE.Contracts.Order;

namespace BooksAPI.BE.Interfaces.Services;

public interface IOrderService
{
    Task CreateOrder(CreateOrderRequest request);

    Task<OrderResponse> GetOrder(int id);

    Task<List<OrderResponse>> GetAllOrders();

    Task<List<OrderResponse>> GetAllOrdersByUserId(string id);

    Task UpdateOrder(int id, UpdateOrderRequest request);

    Task DeleteOrder(int id, string userId);
}