using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface IOrderRepository
{
    public Task CreateOrder(Order order);

    public Task<Order?> GetOrderById(Guid id);

    public Task<List<Order>> GetAllOrders();
    
    public Task UpdateOrder(Order order);

    public Task DeleteOrder(Order order);
}