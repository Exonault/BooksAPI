using BooksAPI.BE.Entities;

namespace BooksAPI.BE.Interfaces.Repositories;

public interface IOrderRepository
{
    Task CreateOrder(Order order);

    Task<Order?> GetOrderById(int id);

    Task<List<Order>> GetAllOrdersByUserId(String userId);

    Task<List<Order>> GetAllOrders();

    Task UpdateOrder(Order order);

    Task DeleteOrder(Order order);
}