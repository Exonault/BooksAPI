using BooksAPI.BE.Contracts.Order;

namespace BooksAPI.BE.Interfaces.Services;

public interface IOrderService
{
    public Task CreateOrder(CreateOrderRequest request);

    public Task<OrderResponse> GetOrder(Guid id);

    public Task<List<OrderResponse>> GetAllOrders();
    
    public Task<List<OrderResponse>> GetAllOrdersByUserId(string id);

    public Task UpdateOrder(Guid id, UpdateOrderRequest request);
    
    public Task DeleteOrder(Guid id, string userId);


}