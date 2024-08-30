using server.Models.DB;
using server.Repositories;
using Microsoft.AspNetCore.Identity;
using server.DTO;

namespace server.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepo _bookRepo;
        private readonly ILogger<BookService> _logger;
        public BookService(IBookRepo bookRepo,ILogger<BookService> logger)
        {
            _bookRepo = bookRepo;
            _logger = logger;
        }

        public async Task<List<BooksDTO>> GetAllBooksAsync()
        {
            try{
                
                var books = await _bookRepo.GetAllBooksAsync();
                var booksDTO = books.Select(book => new BooksDTO{
                    BookId = book.BookId,
                    Title = book.Title!,
                    Price = (decimal)book.Price!,
                    ImageUrl = book.ImageUrl!
                }).ToList();
                return booksDTO;

            }catch(Exception ex){
                _logger.LogError(ex,"Error Occured at GetAllBooksAsync in Service Layer");
                throw;
            }
        }
    }
}