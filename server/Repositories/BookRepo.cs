using Microsoft.EntityFrameworkCore;
using server.Models.DB;

namespace server.Repositories
{
    public class BookRepo : IBookRepo
    {
        private readonly BookStoreDbContext _context;
        private readonly ILogger<BookRepo> _logger;
        public BookRepo(BookStoreDbContext context, ILogger<BookRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            try{
                var books = await _context.Books.ToListAsync();
                return books;
            }catch(Exception ex){
                _logger.LogError(ex,"Error Occured at GetAllBooksAsync in Repo Layer");
                throw;
            }
        }
    }
}