using BookGrid.Models;

namespace BookGrid.DAL.Repositories
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllAsync();
    }
}
