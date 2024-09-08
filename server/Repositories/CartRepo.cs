using Microsoft.EntityFrameworkCore;
using server.Models.DB;
using server.DTO;
using System;
using server.Services;

namespace server.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly BookStoreDbContext _context;
        private readonly IBookService _bookService;

        public CartRepo(BookStoreDbContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }
        public async Task AddBookToCart(int userId,int bookId)
        {
            int cartId = await GetCartId(userId);

            CartItem existingCartItem = _context.CartItems.FirstOrDefault(x => x.BookId == bookId && x.CartId == cartId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                _context.SaveChanges();
            }
            else
            {
                CartItem cartItem = new CartItem
                {
                    CartId = cartId,
                    BookId = bookId,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();
            }
        }

        public async Task RemoveCartItem(int userId,int bookId)
        {
            int cartId = await GetCartId(userId);
            CartItem cartItem = _context.CartItems.FirstOrDefault(x => x.BookId == bookId && x.CartId == cartId);

            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();
        }

        public async Task DeleteOneCartItem(int userId, int bookId)
        {
            int cartId = await GetCartId(userId);
            CartItem cartItem = _context.CartItems.FirstOrDefault(x => x.BookId == bookId && x.CartId == cartId);

            cartItem.Quantity -= 1;
            _context.Entry(cartItem).State = EntityState.Modified;
            if (cartItem.Quantity == 0)
            {
                _context.CartItems.Remove(cartItem);
            }
            
            _context.SaveChanges();
        }

        public async Task<int?> GetCartItemCount(int userId)
        {
            int cartId = await GetCartId(userId);
            int? cartItemCount = 0;

            cartItemCount = _context.CartItems.Where(x => x.CartId == cartId).Sum(x => x.Quantity);

            return cartItemCount;
        }

        public async Task ClearCart(int userId)
        {
            int cartId = await GetCartId(userId);
            List<CartItem> cartItems = _context.CartItems.Where(x => x.CartId == cartId).ToList();

            foreach (CartItem item in cartItems)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }

        }

        public async Task DeleteCart(int userId)
        {
            int cartId = await GetCartId(userId);
            Cart cart = _context.Carts.Find(cartId);
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public async Task<List<CartItemDto>> GetBooksAvailableInCart(int userId)
        {
            int cartId = await GetCartId(userId);
            List<CartItemDto> cartItemList = new List<CartItemDto>();
            List<CartItem> cartItems = _context.CartItems.Where(x => x.CartId == cartId).ToList();

            foreach (CartItem item in cartItems)
            {
                int bookId = (int) item.BookId;
                int quantity = (int)item.Quantity;
                BookDTO book = await _bookService.GetBookByIdAsync(bookId);
                CartItemDto objCartItem = new CartItemDto
                {
                    Book = book,
                    Quantity = quantity
                };

                cartItemList.Add(objCartItem);
            }
            return cartItemList;
        }

        public async Task<int> GetCartId(int userId)
        {
            try
            {
                Cart cart = _context.Carts.FirstOrDefault(x => x.UserId == userId);

                if (cart != null)
                {
                    return cart.CartId;
                }
                else
                {
                    return await CreateCart(userId);
                }

            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CreateCart(int userId)
        {
            try
            {
                Cart shoppingCart = new Cart
                {
                    UserId = userId,
                };

                _context.Carts.Add(shoppingCart);
                _context.SaveChanges();

                return shoppingCart.CartId;
            }
            catch
            {
                throw;
            }
        }


    }
}