using BookGrid.Models;
using BookGrid.Models.DTOS;
using BookGrid.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookGrid.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookViewModel>> GetAllAsync()
        {
            try
            {
                return await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Publisher)
                    .Select(b => new BookViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Genre = b.Genre,
                        AuthorName = b.Author.Name,
                        PublisherName = b.Publisher.Name
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving books", ex);
            }
        }

        public async Task<BookViewModel?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Publisher)
                    .Where(b => b.Id == id)
                    .Select(b => new BookViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Genre = b.Genre,
                        AuthorName = b.Author.Name,
                        PublisherName = b.Publisher.Name
                    }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving book by id", ex);
            }
        }

        public async Task AddAsync(BookDTO dto)
        {
            try
            {
                var author = await _context.Authors.FindAsync(dto.AuthorId);
                var publisher = await _context.Publishers.FindAsync(dto.PublisherId);

                if (author == null)
                    throw new ArgumentException($"Author' not found.");

                if (publisher == null)
                    throw new ArgumentException($"Publisher not found.");

                var book = new Book
                {
                    Title = dto.Title,
                    Genre = dto.Genre,
                    AuthorId = author.Id,
                    PublisherId = publisher.Id
                };

                await _context.Books.AddAsync(book);
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
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<Book?> GetEntityByIdAsync(int id)
        {
            try
            {
                return await _context.Books.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving book entity by id", ex);
            }
        }

        public async Task<Book?> GetByIdRawAsync(int id)
        {
            try
            {
                return await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving book raw by id", ex);
            }
        }
    }
}
