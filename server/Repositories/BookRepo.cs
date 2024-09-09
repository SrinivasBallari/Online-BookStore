using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Models.DB;
 
 
namespace server.Repositories{
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
has context menu