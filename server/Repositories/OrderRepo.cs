using Microsoft.EntityFrameworkCore;
using server.Models.DB;
using server.DTO;

namespace server.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly BookStoreDbContext _context;

        public AuthRepo(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Order.ToListAsync();
        };

        public async Task<List<Order>> GetAllOrdersbyMonthAsync(int month,int year)
        {
            return await _context.Order.Where(o => o.OrderDate.HasValue &&
                                                    o.OrderDate.Value.Month == month &&
                                                    o.OrderDate.Value.Year == year ).ToListAsync();
        };

        public async Task<List<Order>> GetAllOrdersbyEmailAsync(string email)
        {
            return await from order in _context.Order
                         join user in _context.User on order.UserId equals user.UserId
                         where user.Email == email
                         select order;
        };

        public async Task<List<OrderItem>> GetOrderDetailsAsync(int OrderId)
        {
            return await _context.OrderItem.Where(o => o.OrderId == OrderId).ToListAsync();
        };

        public async Task<Order> PlaceOrderAsync(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return order;
        };
    }
}