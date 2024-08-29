using Microsoft.EntityFrameworkCore;
using server.Models.DB;
using server.DTO;
using System;

namespace server.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly BookStoreDbContext _context;

        public OrderRepo(BookStoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersbyMonthAsync(int month,int year)
        {
            return await _context.Orders.Where(o => o.OrderDate.HasValue &&
                                                    o.OrderDate.Value.Month == month &&
                                                    o.OrderDate.Value.Year == year ).ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersbyEmailAsync(string email)
        {
            return await (from order in _context.Orders
                         join user in _context.Users on order.UserId equals user.UserId
                         where user.Email == email
                         select order).ToListAsync();
        }

        public async Task<List<OrderItem>> GetOrderDetailsAsync(int OrderId)
        {
            return await _context.OrderItems.Where(o => o.OrderId == OrderId).ToListAsync();
        }

        public async Task<Order> PlaceOrderAsync(OrderDto order)
        {
            var paymentResult = _context.Payments.Add(new Payment
            {
                Status = "Successful",
                PaymentType = order.PaymentType,
                Amount = order.Total
            });

            await _context.SaveChangesAsync();

            var OrderResult =  _context.Orders.Add(new Order
            {
                UserId = order.UserId,
                PaymentId = paymentResult.Entity.PaymentId,
                Total = order.Total,
                OrderDate = DateOnly.FromDateTime(DateTime.Now)
            });

            await _context.SaveChangesAsync();

            foreach (var item in order.OrderedItems)
            {
                var OrderItemResult = _context.OrderItems.Add(new OrderItem
                {
                    OrderId = OrderResult.Entity.OrderId,
                    BookId = item.BookId,
                    Quantity = item.Quantity
                });
            };

            await _context.SaveChangesAsync();

            return OrderResult.Entity;
            
        }
    }
}