using server.Models.DB;
using server.DTO;

namespace server.Repositories
{
    public interface IOrderRepo
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetAllOrdersbyMonthAsync(int month,int year);
        Task<List<Order>> GetAllOrdersbyEmailAsync(string email);
        Task<List<OrderItem>> GetOrderDetailsAsync(int OrderId);
        Task<Order> PlaceOrderAsync(OrderDto order,string userEmail);
    }
}
