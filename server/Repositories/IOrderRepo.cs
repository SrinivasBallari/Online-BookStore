using server.Models.DB;
using server.DTO;

namespace server.Repositories
{
    public interface IOrderRepo
    {
        Task<List<OrderReturnDto>> GetAllOrdersAsync();
        Task<List<OrderReturnDto>> GetAllOrdersbyMonthAsync(int month,int year);
        Task<List<OrderReturnDto>> GetAllOrdersbyEmailAsync(string email);
        Task<List<OrderItem>> GetOrderDetailsAsync(int OrderId);
        Task<Order> PlaceOrderAsync(OrderDto order,string userEmail);
    }
}
