using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.DTO;
using server.Models.DB;
using server.Services;
using System;
using Microsoft.AspNetCore.Authorization;
using server.Policies;
using server.ActionFilters;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrderController(IOrderService orderService,ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        /// <summary>
        /// Retrieves all order's. 
        /// </summary>
        /// <returns>A list of Orders.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            try
            {
                var result = await _orderService.GetAllOrdersServiceAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        /// <summary>
        /// Retrieves Orders by Date
        /// </summary>
        /// <returns>List of orders based on provided date.</returns>
        [HttpGet("monthly/{month}/{year}")]
        [Authorize]
        public async Task<IActionResult> GetAllOrdersbyMonthAsync(int month, int year)
        {
            try
            {
                var result = await _orderService.GetAllOrdersbyMonthServiceAsync(month, year);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves all orders by email
        /// </summary>
        /// <returns>A List of orders based on provided email</returns>
        [HttpGet("Email/{email}")]
        [Authorize]
        public async Task<IActionResult> GetAllOrdersbyEmailAsync(string email)
        {
            try
            {
                var result = await _orderService.GetAllOrdersbyEmailServiceAsync(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets Order Details based on order ID
        /// </summary>
        /// <returns>Order details of a specific order</returns>
        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrderDetailsAsync(int orderId)
        {
            try
            {
                var result = await _orderService.GetOrderDetailsServiceAsync(orderId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// places an order for a customer
        /// </summary>
        [HttpPost]
        [JwtEmailClaimExtractorFilter]
        [Authorize]
        public async Task<IActionResult> PlaceOrderAsync([FromBody] string paymentType)
        {
            try
            {
                string userEmail = HttpContext.Items["userEmail"] as string;
                
                List<CartItemDto> cartItems = await _cartService.GetBooksAvailableInCartServiceAsync(userEmail);
                decimal? totalCost = 0;
                foreach (CartItemDto item in cartItems)
                {
                    decimal? itemTotalCost = item.Book.Price * item.Quantity;
                    totalCost += itemTotalCost;
                }

                OrderDto order = new OrderDto
                {
                    PaymentType = paymentType,
                    Total = (int)totalCost, 
                    OrderedItems = cartItems
                };

                var result = await _orderService.PlaceOrderServiceAsync(order,userEmail);
                await _cartService.ClearCartServiceAsync(userEmail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}
