using BooksAPI.Contracts.Requests.Order;
using BooksAPI.Contracts.Response.Order;

namespace BooksAPI.Interfaces.Services;

public interface IOrderService
{
    public Task CreateOrder(CreateOrderRequest request);

    public Task<GetOrderResponse> GetOrder(Guid id);

    public Task<List<GetOrderResponse>> GetAllOrders();

    public Task<List<GetOrderResponse>> GetAllOrdersFromPlace(string place);

    public Task UpdateOrder(Guid id, UpdateOrderRequest request);

    public Task DeleteOrder(Guid id);
}