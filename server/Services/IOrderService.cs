using server.DTO;
using server.Models.DB;

namespace server.Services
{
    public interface IOrderService
    {
        Task<List<OrderReturnDto>> AllOrdersServiceAsync();
        Task<List<OrderReturnDto>> AllOrdersbyMonthServiceAsync(int month,int year);
        Task<List<OrderReturnDto>> AllOrdersbyEmailServiceAsync(string email);
        Task<List<OrderItemDto>> OrderDetailsServiceAsync(int OrderId);
        Task<Order> PlaceOrderServiceAsync(OrderDto order);
    }

}