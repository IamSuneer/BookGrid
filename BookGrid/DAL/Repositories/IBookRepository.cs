using BookGrid.Models;
using BookGrid.Models.DTOS;
using BookGrid.Models.ViewModels;

namespace BookGrid.DAL.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookViewModel>> GetAllAsync();
        Task<BookViewModel?> GetByIdAsync(int id);
        Task AddAsync(BookDTO dto);
        Task SaveAsync();
        Task<Book?> GetEntityByIdAsync(int id);
        Task<Book?> GetByIdRawAsync(int id);
    }
}
