﻿using BooksAPI.BE.Data;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Repositories;

public class OrderRepository:IOrderRepository
{
    private ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateOrder(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetOrderById(Guid id)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task UpdateOrder(Order order)
    {
        _dbContext.Entry(order).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrder(Order order)
    {
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}