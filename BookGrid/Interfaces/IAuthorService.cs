using BookGrid.Models;

namespace BookGrid.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAsync();

    }
}
