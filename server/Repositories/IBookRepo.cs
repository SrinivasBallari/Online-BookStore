using server.Models.DB;

namespace server.Repositories
{
    public interface IBookRepo
    {
        Task<List<Book>> GetAllBooksAsync();
    }
}
