using BookGrid.Models;
using Microsoft.EntityFrameworkCore;

namespace BookGrid.DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            try
            {
                return await _context.Authors
                    .Include(a => a.Books)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Error retrieving authors", ex);
            }

        }
    }
}
