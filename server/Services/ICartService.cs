using server.DTO;
using server.Models.DB;

namespace server.Services
{
    public interface ICartService
    {
        Task AddBookToCartServiceAsync(string userEmail, int bookId);
        Task RemoveCartItemServiceAsync(string userEmail, int bookId);
        Task DeleteOneCartItemServiceAsync(string userEmail, int bookId);
        Task<int?> GetCartItemCountServiceAsync(string userEmail);
        Task ClearCartServiceAsync(string userEmail);
        Task DeleteCartServiceAsync(string userEmail);
        Task<List<CartItemDto>> GetBooksAvailableInCartServiceAsync(string userEmail);
    }

}