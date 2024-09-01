using System.Threading.Tasks;
using server.Models.DB;



namespace server.Repositories{
    public interface IAuthorRepo
{
    Task<Author> GetByIdAsync(int authorId);
    Task AddAsync(Author author);
    
      Task<IEnumerable<Author>> GetAllAuthorsAsync(); 
}

}