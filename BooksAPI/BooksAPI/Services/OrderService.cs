using AutoMapper;
using BooksAPI.Contracts.Requests.Order;
using BooksAPI.Contracts.Response.Comic;
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


    public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
    {
        Order order = _mapper.Map<Order>(request);

        ValidationResult validationResult = _validator.Validate(order);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        try
        {
            order = await _orderRepository.CreateOrder(order);
        }
        catch (System.Exception)
        {
            throw;
        }

        return _mapper.Map<CreateOrderResponse>(order);
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

    public async Task<UpdateOrderResponse> UpdateOrder(Guid id, UpdateOrderRequest request)
    {
        Order? order = await _orderRepository.GetOrderById(id);

        if (order is null)
        {
            throw new ResourceNotFoundException("Order with id doesn't exist.");
        }

        Order updatedOrder = _mapper.Map<Order>(request);

        ValidationResult validationResult = _validator.Validate(updatedOrder);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        try
        {
            _mapper.Map(request, order);
            order = await _orderRepository.UpdateOrder(order);
        }
        catch (System.Exception)
        {
            throw;
        }

        return _mapper.Map<UpdateOrderResponse>(order);
    }

    public async Task<DeleteOrderResponse> DeleteOrder(Guid id)
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

        return _mapper.Map<DeleteOrderResponse>(order);
    }
}