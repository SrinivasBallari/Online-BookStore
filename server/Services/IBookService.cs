using server.DTO;
using server.Models.DB;

namespace server.Services
{
    public interface IBookService
    {
        Task<List<BooksDTO>> GetAllBooksAsync();
    }

}