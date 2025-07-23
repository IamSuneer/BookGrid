using BookGrid.Models;

namespace BookGrid.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<Publisher>> GetAllAsync();
    }
}
