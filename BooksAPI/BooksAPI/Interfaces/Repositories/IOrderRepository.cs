using BooksAPI.Entities;

namespace BooksAPI.Interfaces.Repositories;

public interface IOrderRepository
{
    public Task<Order> CreateOrder(Order order);

    public Task<Order?> GetOrderById(Guid id);

    public Task<List<Order>> GetAllOrders();

    public Task<List<Order>> GetAllOrdersByPlace(String place);

    public Task<Order> UpdateOrder(Order order);

    public Task DeleteOrder(Order order);
}