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
    public class CartController : Controller
    {
        readonly ICartService _cartService;
       // readonly IBookService _bookService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            
        }

        /// <summary>
        /// Get the list of items in the shopping cart
        /// </summary>
        
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [JwtEmailClaimExtractorFilter]
        public async Task<IActionResult> GetBooksAvailableInCart()
        {
            try
            {
                string userEmail = HttpContext.Items["userEmail"] as string;
                List<CartItemDto> result = await _cartService.GetBooksAvailableInCartServiceAsync(userEmail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Add a single item into the shopping cart. If the item already exists, increase the quantity by one
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns>Count of items in the shopping cart</returns>
        [HttpPost("{bookId}")]
        [Authorize]
        [JwtEmailClaimExtractorFilter]
        public async Task<IActionResult> AddBookToCart(int bookId)
        {
            try
            {
                string userEmail = HttpContext.Items["userEmail"] as string;
                await _cartService.AddBookToCartServiceAsync(userEmail, bookId);
                int result = await _cartService.GetCartItemCountServiceAsync(userEmail) ?? 0;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }


        }

        /// <summary>
        /// Reduces the quantity by one for an item in shopping cart
        /// </summary>
        
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPut("{bookId}")]
        [JwtEmailClaimExtractorFilter]
        public async Task<IActionResult> DeleteOneCartItem(int bookId)
        {
            try
            {
                string userEmail = HttpContext.Items["userEmail"] as string;
                await _cartService.DeleteOneCartItemServiceAsync(userEmail, bookId);
                int result = await _cartService.GetCartItemCountServiceAsync(userEmail) ?? 0;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        /// <summary>
        /// Delete a single item from the cart 
        /// </summary>
        
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpDelete("{bookId}")]
        [JwtEmailClaimExtractorFilter]
        public async Task<IActionResult> RemoveCartItem(int bookId)
        {
            try
            {
                string userEmail = HttpContext.Items["userEmail"] as string;
                await _cartService.RemoveCartItemServiceAsync(userEmail, bookId);
                int result = await _cartService.GetCartItemCountServiceAsync(userEmail) ?? 0;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        /// <summary>
        /// Clear the shopping cart
        /// </summary>
       
        /// <returns></returns>
        [HttpDelete]
        [JwtEmailClaimExtractorFilter]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                string userEmail = HttpContext.Items["userEmail"] as string;
                await _cartService.ClearCartServiceAsync(userEmail);
                int result = await _cartService.GetCartItemCountServiceAsync(userEmail) ?? 0;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }
    }
}