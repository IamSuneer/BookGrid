using BookGrid.Models;
using Microsoft.EntityFrameworkCore;

namespace BookGrid.DAL.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _context;

        public PublisherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            try
            {
                return await _context.Publishers
                    .Include(a => a.Books)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception("Error retrieving publishers", ex);
            }
        }
    }
}
