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
        public async Task<List<OrderReturnDto>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .Select(o => new OrderReturnDto
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    PaymentId = o.PaymentId,
                    OrderDate = o.OrderDate,
                    Total = o.Total,
                    OrderedItems = o.OrderItems.Select(oi => new CartItemDto
                    {
                        Book = new BookDTO
                        {
                            BookId = oi.Book.BookId,
                            Title = oi.Book.Title,
                            AuthorId = oi.Book.AuthorId,
                            AuthorName = oi.Book.Author != null ? oi.Book.Author.AuthorName : null,
                            PagesCount = oi.Book.PagesCount,
                            Language = oi.Book.Language,
                            PublisherId = oi.Book.PublisherId,
                            PublisherName = oi.Book.Publisher != null ? oi.Book.Publisher.PublisherName : null,
                            PublishedDate = oi.Book.PublishedDate,
                            PublishedVersion = oi.Book.PublishedVersion,
                            Price = oi.Book.Price,
                            Description = oi.Book.Description,
                            ImageUrl = oi.Book.ImageUrl
                        },
                        Quantity = oi.Quantity ?? 0
                    }).ToList()
                }).ToListAsync();
        }


        public async Task<List<OrderReturnDto>> GetAllOrdersbyMonthAsync(int month,int year)
        {
            return await _context.Orders
              .Include(o => o.OrderItems)
              .ThenInclude(oi => oi.Book)
              .Where(o => o.OrderDate.HasValue &&
                          o.OrderDate.Value.Month == month &&
                          o.OrderDate.Value.Year == year)
              .Select(o => new OrderReturnDto
              {
                  OrderId = o.OrderId,
                  UserId = o.UserId,
                  PaymentId = o.PaymentId,
                  OrderDate = o.OrderDate,
                  Total = o.Total,
                  OrderedItems = o.OrderItems.Select(oi => new CartItemDto
                  {
                      Book = new BookDTO
                      {
                          BookId = oi.Book.BookId,
                          Title = oi.Book.Title,
                          AuthorId = oi.Book.AuthorId,
                          AuthorName = oi.Book.Author != null ? oi.Book.Author.AuthorName : null,
                          PagesCount = oi.Book.PagesCount,
                          Language = oi.Book.Language,
                          PublisherId = oi.Book.PublisherId,
                          PublisherName = oi.Book.Publisher != null ? oi.Book.Publisher.PublisherName : null,
                          PublishedDate = oi.Book.PublishedDate,
                          PublishedVersion = oi.Book.PublishedVersion,
                          Price = oi.Book.Price,
                          Description = oi.Book.Description,
                          ImageUrl = oi.Book.ImageUrl
                      },
                      Quantity = oi.Quantity ?? 0
                  }).ToList()
              }).ToListAsync();
        }

        public async Task<List<OrderReturnDto>> GetAllOrdersbyEmailAsync(string email)
        {
            return await (from order in _context.Orders
                          join user in _context.Users on order.UserId equals user.UserId
                          where user.Email == email
                          select new OrderReturnDto
                          {
                              OrderId = order.OrderId,
                              UserId = order.UserId,
                              PaymentId = order.PaymentId,
                              OrderDate = order.OrderDate,
                              Total = order.Total,
                              OrderedItems = order.OrderItems.Select(oi => new CartItemDto
                              {
                                  Book = new BookDTO
                                  {
                                      BookId = oi.Book.BookId,
                                      Title = oi.Book.Title,
                                      AuthorId = oi.Book.AuthorId,
                                      AuthorName = oi.Book.Author != null ? oi.Book.Author.AuthorName : null,
                                      PagesCount = oi.Book.PagesCount,
                                      Language = oi.Book.Language,
                                      PublisherId = oi.Book.PublisherId,
                                      PublisherName = oi.Book.Publisher != null ? oi.Book.Publisher.PublisherName : null,
                                      PublishedDate = oi.Book.PublishedDate,
                                      PublishedVersion = oi.Book.PublishedVersion,
                                      Price = oi.Book.Price,
                                      Description = oi.Book.Description,
                                      ImageUrl = oi.Book.ImageUrl
                                  },
                                  Quantity = oi.Quantity ?? 0
                              }).ToList()
                          }).ToListAsync();
        }

        public async Task<List<OrderItem>> GetOrderDetailsAsync(int OrderId)
        {
            return await _context.OrderItems.Where(o => o.OrderId == OrderId).ToListAsync();
        }

        public async Task<Order> PlaceOrderAsync(OrderDto order,string userEmail)
        {
            var paymentResult = _context.Payments.Add(new Payment
            {
                Status = "Successful",
                PaymentType = order.PaymentType,
                Amount = order.Total
            });

            await _context.SaveChangesAsync();
            var user = _context.Users.Where(u => u.Email == userEmail).FirstOrDefault();
            
            var OrderResult =  _context.Orders.Add(new Order
            {
                UserId = user.UserId,
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
                    BookId = item.Book.BookId,
                    Quantity = item.Quantity
                });
            };

            await _context.SaveChangesAsync();



            return OrderResult.Entity;
            
        }
    }
}