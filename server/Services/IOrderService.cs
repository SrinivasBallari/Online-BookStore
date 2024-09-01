using server.DTO;
using server.Models.DB;

namespace server.Services
{
    public interface IOrderService
    {
        Task<List<OrderReturnDto>> GetAllOrdersServiceAsync();
        Task<List<OrderReturnDto>> GetAllOrdersbyMonthServiceAsync(int month,int year);
        Task<List<OrderReturnDto>> GetAllOrdersbyEmailServiceAsync(string email);
        Task<List<OrderItemDto>> GetOrderDetailsServiceAsync(int OrderId);
        Task<Order> PlaceOrderServiceAsync(OrderDto order);
    }

}