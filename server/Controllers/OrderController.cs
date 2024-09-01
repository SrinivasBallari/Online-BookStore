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

namespace server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "customer")]
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

        [HttpGet("monthly/{month}/{year}")]
        
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

        [HttpGet("Email/{email}")]
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

        [HttpGet("{orderId}")]
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

        [HttpPost]
        public async Task<IActionResult> PoPlaceOrderAsyncst([FromBody] OrderDto order)
        {
            try
            {
                var result = await _orderService.PlaceOrderServiceAsync(order);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}
