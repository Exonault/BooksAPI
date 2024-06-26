﻿using BooksAPI.BE.Data;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Repositories;

public class OrderRepository:IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateOrder(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetOrderById(int id)
    {
        return await _dbContext.Orders
            .Include(o=> o.User)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Order>> GetAllOrdersByUserId(string userId)
    {
        return await _dbContext.Orders
            .Include(o => o.User)
            .Where(o => o.User.Id == userId)
            .OrderBy(o => o.Date)
            .ToListAsync();
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _dbContext.Orders
            .Include(o=> o.User)
            .OrderBy(o => o.Id)
            .ToListAsync();
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