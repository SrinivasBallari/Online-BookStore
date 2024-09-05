 using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Models.DB;


namespace server.Repositories{
   public class PublisherRepo : IPublisherRepo
{
    private readonly BookStoreDbContext _context;

    public PublisherRepo(BookStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Publisher> GetByIdAsync(int publisherId)
    {
        return await _context.Publishers.FindAsync(publisherId);
    }

    public async Task AddAsync(Publisher publisher)
    {
        await _context.Publishers.AddAsync(publisher);
        await _context.SaveChangesAsync();
    }

      public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
    {
        return await _context.Publishers.ToListAsync();
    }

     
       
    }

}