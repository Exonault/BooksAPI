using BooksAPI.Contracts.Response.Order;
using BooksAPI.Interfaces.Services;

namespace BooksAPI.Services;

public class OrderService:IOrderService
{
    public Task<CreateOrderResponse> CreateComic(CreateOrderResponse request)
    {
        throw new NotImplementedException();
    }

    public Task<GetOrderResponse> GetComic(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetOrderResponse>> GetAllComics()
    {
        throw new NotImplementedException();
    }

    public Task<UpdateOrderResponse> UpdateComic(Guid id, UpdateOrderResponse request)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteOrderResponse> DeleteComic(Guid id)
    {
        throw new NotImplementedException();
    }
}