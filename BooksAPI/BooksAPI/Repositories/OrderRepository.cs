using BooksAPI.Data;
using BooksAPI.Entities;
using BooksAPI.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> GetOrderById(Guid id)
    {
        return await _dbContext.Orders.FindAsync(id);
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task<Order> UpdateOrder(Order order)
    {
        _dbContext.Entry(order).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task DeleteOrder(Order order)
    {
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}