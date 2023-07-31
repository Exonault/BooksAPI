using BooksAPI.Contracts.Requests.Order;
using BooksAPI.Contracts.Response.Order;

namespace BooksAPI.Interfaces.Services;

public interface IOrderService
{
    public Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request);

    public Task<GetOrderResponse> GetOrder(Guid id);

    public Task<List<GetOrderResponse>> GetAllOrders();

    public Task<UpdateOrderResponse> UpdateOrder(Guid id, UpdateOrderRequest request);

    public Task<DeleteOrderResponse> DeleteOrder(Guid id);
}