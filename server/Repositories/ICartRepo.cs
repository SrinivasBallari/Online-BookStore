
using server.DTO;
using server.Models.DB;

namespace server.Repositories
{
    public interface ICartRepo
    {
        Task AddBookToCart(int userId, int bookId);
        Task RemoveCartItem(int userId, int bookId);
        Task DeleteOneCartItem(int userId, int bookId);
        Task<int?> GetCartItemCount(int userId);
        Task ClearCart(int userId);
        Task DeleteCart(int userId);
        Task<List<CartItemDto>> GetBooksAvailableInCart(int userId);
        Task<int> GetCartId(int userId);
        Task<int> CreateCart(int userId);
    }
}
