using BookAPI.Presentation.Models;

namespace BookAPI.Presentation.Interfaces;

public interface IOrderService
{
    Task<HttpResponseMessage> CreateOrder(ModifyOrderModel model);

    Task<HttpResponseMessage> GetAllOrders();

    Task<HttpResponseMessage> GetOrder(string id);

    Task<HttpResponseMessage> UpdateOrder(string id, ModifyOrderModel model);

    Task<HttpResponseMessage> DeleteOrder(string id);
}