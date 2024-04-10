using AutoMapper;
using BooksAPI.BE.Contracts.Order;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace BooksAPI.BE.Services;

public class OrderServices : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<Order> _validator;
    private readonly IUserRepository _userRepository;

    public OrderServices(IOrderRepository orderRepository, IMapper mapper, IValidator<Order> validator, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _validator = validator;
        _userRepository = userRepository;
    }

    public async Task CreateOrder(CreateOrderRequest request)
    {
        User? user = await _userRepository.GetById(request.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.ValidationMessages.UserNotFound);
        }

        Order order = _mapper.Map<Order>(request);

        order.User = user;

        ValidationResult validationResult = await _validator.ValidateAsync(order);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _orderRepository.CreateOrder(order);
    }

    public async Task<OrderResponse> GetOrder(Guid id)
    {
        Order? order = await _orderRepository.GetOrderById(id);

        if (order is null)
        {
            throw new ResourceNotFoundException(OrderMessages.NoOrderWithId);
        }

        OrderResponse orderResponse = _mapper.Map<OrderResponse>(order);

        return orderResponse;
    }

    public async Task<List<OrderResponse>> GetAllOrders()
    {
        List<Order> allOrders = await _orderRepository.GetAllOrders();

        List<OrderResponse> response = _mapper.Map<List<OrderResponse>>(allOrders);

        return response;
    }

    public async Task<List<OrderResponse>> GetAllOrdersByUserId(string userId)
    {
        User? user = await _userRepository.GetById(userId);
        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.ValidationMessages.UserNotFound);
        }

        List<Order> orders = await _orderRepository.GetAllOrdersByUserId(userId);
        List<OrderResponse> response = _mapper.Map<List<OrderResponse>>(orders);

        return response;
    }


    public async Task UpdateOrder(Guid id, UpdateOrderRequest request)
    {
        User? user = await _userRepository.GetById(request.UserId);
        
        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.ValidationMessages.UserNotFound);
        }

        Order? order = await _orderRepository.GetOrderById(id);

        if (order is null)
        {
            throw new ResourceNotFoundException(OrderMessages.NoOrderWithId);
        }

        Order updatedOrder = _mapper.Map<Order>(request);

        updatedOrder.User = user;

        ValidationResult validationResult = await _validator.ValidateAsync(updatedOrder);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        _mapper.Map(request, order);

        await _orderRepository.UpdateOrder(order);
    }

    public async Task DeleteOrder(Guid id, string userId)
    {
        User? user = await _userRepository.GetById(userId);
        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.ValidationMessages.UserNotFound);
        }

        Order? order = await _orderRepository.GetOrderById(id);

        if (order is null)
        {
            throw new ResourceNotFoundException(OrderMessages.NoOrderWithId);
        }

        if (order.User.Id != userId)
        {
            throw new InvalidOperationException(OrderMessages.DeleteImpossible);
        }

        await _orderRepository.DeleteOrder(order);
    }
}