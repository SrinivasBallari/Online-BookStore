 using System.Collections.Generic;
using System.Threading.Tasks;
using server.Models.DB;
namespace server.Repositories{
   

public interface ITagRepo
{
    Task<IEnumerable<Tag>> GetAllTagsAsync();
    Task<Tag> GetByIdAsync(int tagId);
    Task<IEnumerable<Book>> GetBooksByTagIdAsync(int tagId);
    Task AddAsync(Tag tag);
    Task<Tag> GetByNameAsync(string tagName);
}

}