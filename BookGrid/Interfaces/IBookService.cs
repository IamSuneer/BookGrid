using BookGrid.Models.DTOS;
using BookGrid.Models.ViewModels;

namespace BookGrid.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookViewModel>> GetBooksCachedAsync();
        Task<BookViewModel?> GetBookByIdAsync(int id);
        Task AddBookAsync(BookDTO dto);
        Task SaveAsync();
        Task<BookDTO?> GetBookDtoByIdAsync(int id);
        Task UpdateBookAsync(int id, BookDTO dto);
    }
}
