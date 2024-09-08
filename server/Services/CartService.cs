using server.Models.DB;
using server.Repositories;
using Microsoft.AspNetCore.Identity;
using server.DTO;
using System;
using Microsoft.AspNetCore.Mvc;

namespace server.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly IAuthRepo _authRepo;

        public CartService(ICartRepo cartRepo, IAuthRepo authRepo)
        {
            _cartRepo = cartRepo;
            _authRepo = authRepo;
        }

        public async Task AddBookToCartServiceAsync(string userEmail, int bookId)
        {
            var user = await _authRepo.GetUserByEmailAsync(userEmail);
            await _cartRepo.AddBookToCart(user.UserId, bookId);
        }

        

        public async Task RemoveCartItemServiceAsync(string userEmail, int bookId)
        {
            var user = await _authRepo.GetUserByEmailAsync(userEmail);
            await _cartRepo.RemoveCartItem(user.UserId, bookId);
        }

        public async Task DeleteOneCartItemServiceAsync(string userEmail, int bookId)
        {
            var user = await _authRepo.GetUserByEmailAsync(userEmail);
            await _cartRepo.DeleteOneCartItem(user.UserId, bookId);
        }

        public async Task<int?> GetCartItemCountServiceAsync(string userEmail)
        {
            var user = await _authRepo.GetUserByEmailAsync(userEmail);
            return await _cartRepo.GetCartItemCount(user.UserId);

        }

        public async Task ClearCartServiceAsync(string userEmail)
        {
            var user = await _authRepo.GetUserByEmailAsync(userEmail);
            await _cartRepo.ClearCart(user.UserId);
        }

        public async Task DeleteCartServiceAsync(string userEmail)
        {
            var user = await _authRepo.GetUserByEmailAsync(userEmail);
            await _cartRepo.DeleteCart(user.UserId);
        }

        public async Task<List<CartItemDto>> GetBooksAvailableInCartServiceAsync(string userEmail)
        {
            var user = await _authRepo.GetUserByEmailAsync(userEmail);
            return await _cartRepo.GetBooksAvailableInCart(user.UserId);
        }
    }
}