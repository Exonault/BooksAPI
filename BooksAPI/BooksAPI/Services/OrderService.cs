using AutoMapper;
using BooksAPI.Contracts.Requests.Order;
using BooksAPI.Contracts.Response.Order;
using BooksAPI.Entities;
using BooksAPI.Exception;
using BooksAPI.Interfaces.Repositories;
using BooksAPI.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;

namespace BooksAPI.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<Order> _validator;
    private readonly IMapper _mapper;


    public OrderService(IOrderRepository orderRepository, IValidator<Order> validator, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _validator = validator;
        _mapper = mapper;
    }


    public async Task CreateOrder(CreateOrderRequest request)
    {
        Order order = _mapper.Map<Order>(request);

        ValidationResult validationResult = await _validator.ValidateAsync(order);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        try
        {
            await _orderRepository.CreateOrder(order);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<GetOrderResponse> GetOrder(Guid id)
    {
        Order? order = await _orderRepository.GetOrderById(id);

        if (order is null)
        {
            throw new ResourceNotFoundException("Order with id doesn't exist.");
        }

        return _mapper.Map<GetOrderResponse>(order);
    }

    public async Task<List<GetOrderResponse>> GetAllOrders()
    {
        List<Order> orders = await _orderRepository.GetAllOrders();


        return _mapper.Map<List<GetOrderResponse>>(orders);
    }

    public async Task<List<GetOrderResponse>> GetAllOrdersFromPlace(string place)
    {
        List<Order> allOrdersByPlace = await _orderRepository.GetAllOrdersByPlace(place);

        return _mapper.Map<List<GetOrderResponse>>(allOrdersByPlace);
    }

    public async Task UpdateOrder(Guid id, UpdateOrderRequest request)
    {
        Order? order = await _orderRepository.GetOrderById(id);

        if (order is null)
        {
            throw new ResourceNotFoundException("Order with id doesn't exist.");
        }

        Order updatedOrder = _mapper.Map<Order>(request);

        ValidationResult validationResult = await _validator.ValidateAsync(updatedOrder);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        try
        {
            _mapper.Map(request, order);
            await _orderRepository.UpdateOrder(order);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task DeleteOrder(Guid id)
    {
        Order? order = await _orderRepository.GetOrderById(id);

        if (order is null)
        {
            throw new ResourceNotFoundException("Order with id doesn't exist.");
        }

        try
        {
            await _orderRepository.DeleteOrder(order);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}