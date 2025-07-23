using BookGrid.DAL.Repositories;
using BookGrid.Interfaces;
using BookGrid.Models;
using BookGrid.Models.DTOS;
using BookGrid.Models.ViewModels;

namespace BookGrid.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        private readonly ICacheService _cache;
        private const string CacheKey = "books";

        public BookService(IBookRepository repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<IEnumerable<BookViewModel>> GetBooksCachedAsync()
        {
            try
            {
                var books = _cache.Get<IEnumerable<BookViewModel>>(CacheKey);
                if (books == null)
                {
                    books = await _repo.GetAllAsync();
                    _cache.Set(CacheKey, books, TimeSpan.FromMinutes(5));
                }
                return books;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting cached books", ex);
            }
        }

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            try
            {
                return await _repo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting book by ID", ex);
            }
        }

        public async Task AddBookAsync(BookDTO dto)
        {
            try
            {
                var book = new Book
                {
                    Title = dto.Title,
                    Genre = dto.Genre,
                    AuthorId = dto.AuthorId,
                    PublisherId = dto.PublisherId
                };
                await _repo.AddAsync(dto);
                _cache.Remove(CacheKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding book", ex);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _repo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving book", ex);
            }
        }

        public async Task<BookDTO?> GetBookDtoByIdAsync(int id)
        {
            try
            {
                var book = await _repo.GetByIdRawAsync(id);
                if (book == null) return null;

                return new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Genre = book.Genre,
                    AuthorId = book.AuthorId,
                    PublisherId = book.PublisherId
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving book for patch", ex);
            }
        }

        public async Task UpdateBookAsync(int id, BookDTO dto)
        {
            try
            {
                var book = await _repo.GetEntityByIdAsync(id);
                if (book == null)
                    throw new ArgumentException("Book not found");

                book.Title = dto.Title;
                book.Genre = dto.Genre;
                book.AuthorId = dto.AuthorId;
                book.PublisherId = dto.PublisherId;

                _cache.Remove(CacheKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating book", ex);
            }
        }

    }
}
