using server.Models.DB;
using server.Repositories;
using Microsoft.AspNetCore.Identity;
using server.DTO;
using System;

namespace server.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<List<OrderReturnDto>> GetAllOrdersServiceAsync()
        {
            return await _orderRepo.GetAllOrdersAsync();
          
        }
        public async Task<List<OrderReturnDto>> GetAllOrdersbyMonthServiceAsync(int month,int year)
        {
            return await _orderRepo.GetAllOrdersbyMonthAsync(month,year);

        }
        public async Task<List<OrderReturnDto>> GetAllOrdersbyEmailServiceAsync(string email)
        {
            return await _orderRepo.GetAllOrdersbyEmailAsync(email);
          
        }

        public async Task<List<OrderItemDto>> GetOrderDetailsServiceAsync(int OrderId)
        {
            var result = await _orderRepo.GetOrderDetailsAsync(OrderId);
            var orderItemDtos = result.Select(orderItem => new OrderItemDto
           {
               OrderItemId = orderItem.OrderItemId,
               OrderId = orderItem.OrderId,
               BookId = orderItem.BookId,
               Quantity = orderItem.Quantity  
           }).ToList();

            return orderItemDtos;
        }

        public async Task<Order> PlaceOrderServiceAsync(OrderDto order,string userEmail)
        {
            var result = await _orderRepo.PlaceOrderAsync(order,userEmail);

            return result;
        }
    }
}