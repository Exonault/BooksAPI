using BooksAPI.Contracts.Response.Order;

namespace BooksAPI.Interfaces.Services;

public interface IOrderService
{
    public Task<CreateOrderResponse> CreateComic(CreateOrderResponse request);

    public Task<GetOrderResponse> GetComic(Guid id);

    public Task<List<GetOrderResponse>> GetAllComics();

    public Task<UpdateOrderResponse> UpdateComic(Guid id, UpdateOrderResponse request);

    public Task<DeleteOrderResponse> DeleteComic(Guid id);
}