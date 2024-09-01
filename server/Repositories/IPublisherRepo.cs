using System.Threading.Tasks;
using server.Models.DB;

namespace server.Repositories{
    

public interface IPublisherRepo
{
    Task<Publisher> GetByIdAsync(int publisherId);
    Task AddAsync(Publisher publisher);
   

    Task<IEnumerable<Publisher>> GetAllPublishersAsync();
}


}