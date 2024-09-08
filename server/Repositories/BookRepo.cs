using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.DTO;
using server.Models.DB;


namespace server.Repositories
{

    public class BookRepo : IBookRepo
    {
        private readonly BookStoreDbContext _context;

        public BookRepo(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Tags)
                .ToListAsync();
        }

        public async Task<PagedBooksResponse<PaginatedBooksDTO>> GetPageWiseBooksAsync(int page, int pageSize, List<string> genresList,List<string> authorsList)
        {

            var query = _context.Books
                .Include(b => b.Tags)
                .Include(b => b.Author)
                .AsQueryable();
            
            if (genresList.Count > 0)
            {
                query = query.Where(b => b.Tags.Any(t => genresList.Contains(t.Tag1)));
            }

            if(authorsList.Count > 0){
                query = query.Where(b => authorsList.Contains(b.Author.AuthorName));
            }

            var totalCount = await query.CountAsync();

            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new PaginatedBooksDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    AuthorName = b.Author.AuthorName,
                    TagNames = b.Tags.Select(t => t.Tag1).ToList(),
                    Price = b.Price,
                    ImageUrl = b.ImageUrl,
                    Language = b.Language,
                })
                .ToListAsync();

            return new PagedBooksResponse<PaginatedBooksDTO>
            {
                Books = books,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }


        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(b => b.BookId == bookId);
        }
        public async Task<Book?> GetBookByAttributesAsync(string title, int? authorId, int? publisherId, DateOnly? publishedDate)
        {
            return await _context.Books.FirstOrDefaultAsync(b =>
                b.Title == title &&
                b.AuthorId == authorId &&
                b.PublisherId == publisherId &&
                b.PublishedDate == publishedDate
            );
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchString)
        {

            var searchTerms = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);


            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Tags)
                .Where(b => searchTerms.Any(term =>
                                b.Title.Contains(term) ||
                                b.Tags.Any(t => t.Tag1.Contains(term)) ||
                                b.Author.AuthorName.Contains(term) ||
                                b.Publisher.PublisherName.Contains(term) ||
                                b.Language.Contains(term)))
                .ToListAsync();
        }


        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _context.Books
                .Include(b => b.Tags)
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.BookId == bookId);

            if (book != null)
            {

                foreach (var tag in book.Tags.ToList())
                {
                    tag.Books.Remove(book);
                }


                if (book.Author != null)
                {
                    book.Author.Books.Remove(book);

                }


                if (book.Publisher != null)
                {
                    book.Publisher.Books.Remove(book);

                }


                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        

    }

}