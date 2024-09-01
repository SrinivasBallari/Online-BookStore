 using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Models.DB;


namespace server.Repositories{
   
public class TagRepo : ITagRepo
{
    private readonly BookStoreDbContext _context;

    public TagRepo(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllTagsAsync()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<Tag> GetByIdAsync(int tagId)
    {
        return await _context.Tags.FindAsync(tagId);
    }

    public async Task<IEnumerable<Book>> GetBooksByTagIdAsync(int tagId)
    {
        var tag = await _context.Tags
            .Include(t => t.Books)
            .FirstOrDefaultAsync(t => t.TagId == tagId);

        return tag?.Books ?? Enumerable.Empty<Book>();
    }

    public async Task<Tag> GetByNameAsync(string tagName)
{
    return await _context.Tags.FirstOrDefaultAsync(t => t.Tag1 == tagName);
}

    public async Task AddAsync(Tag tag)
    {
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
    }

    }

}