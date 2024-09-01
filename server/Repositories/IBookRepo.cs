using server.Models.DB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace server.Repositories
{
   


public interface IBookRepo
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int bookId);
    Task<IEnumerable<Book>> SearchBooksAsync(string searchString);
    Task AddBookAsync(Book book);
    Task<bool> DeleteBookAsync(int bookId);
}


}
