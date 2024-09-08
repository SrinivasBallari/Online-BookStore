using server.DTO;
using server.Models.DB;

namespace server.Repositories
{
    public interface IBookRepo
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<PagedBooksResponse<PaginatedBooksDTO>> GetPageWiseBooksAsync(int page,int pageSize, List<string> genresList,List<string> authorsList);
        Task<Book> GetBookByIdAsync(int bookId);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchString);
        Task AddBookAsync(Book book);
        Task<bool> DeleteBookAsync(int bookId);
        Task<Book?> GetBookByAttributesAsync(string title, int? authorId, int? publisherId, DateOnly? publishedDate);
        
    }

}
