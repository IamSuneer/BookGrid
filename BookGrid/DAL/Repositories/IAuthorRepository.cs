using BookGrid.Models;

namespace BookGrid.DAL.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
    }
}
