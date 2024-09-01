using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Models.DB;

namespace server.Repositories{
    

    public class AuthorRepo : IAuthorRepo
{
    private readonly BookStoreDbContext _context;

    public AuthorRepo(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Author> GetByIdAsync(int authorId)
    {
        return await _context.Authors.FindAsync(authorId);
    }

    public async Task AddAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _context.Authors.ToListAsync();
    }

  
}

}