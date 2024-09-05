using System.Collections.Generic;
using System.Threading.Tasks;
using server.DTO;
using server.Models.DB;


namespace server.Services{

public interface IBookService
{
    Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    Task<BookDTO> GetBookByIdAsync(int bookId);
    Task<IEnumerable<BookDTO>> SearchBooksAsync(string searchString);
    Task<IEnumerable<Tag>> GetAllCategoriesAsync();
    Task<IEnumerable<BookDTO>> GetBooksByCategoryAsync(int tagId);
    Task<IEnumerable<BookDTO>> GetSimilarBooksAsync(int bookId);
    Task<BookResponseDTO> AddBookAsync(BookDTO bookDTO);
    Task<bool> DeleteBookAsync(int bookId);
    Task<BookResponseDTO> UpdateBookAsync(int bookId, BookDTO bookDTO);  

    Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync();
    Task<IEnumerable<PublisherDTO>> GetAllPublishersAsync();
}

   
}