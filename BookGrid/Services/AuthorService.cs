using BookGrid.DAL.Repositories;
using BookGrid.Interfaces;
using BookGrid.Models;
using BookGrid.Models.ViewModels;

namespace BookGrid.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repo;
        private readonly ICacheService _cache;
        private const string CacheKey = "authors";

        public AuthorService(IAuthorRepository repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            try
            {
                var authors = _cache.Get<IEnumerable<Author>>(CacheKey);
                if (authors == null)
                {
                    authors = await _repo.GetAllAsync();
                    _cache.Set(CacheKey, authors, TimeSpan.FromMinutes(5));
                }
                return authors;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting cached books", ex);
            }
        }
    }
}
