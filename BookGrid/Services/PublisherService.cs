using BookGrid.DAL.Repositories;
using BookGrid.Interfaces;
using BookGrid.Models;
using BookGrid.Models.ViewModels;

namespace BookGrid.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repo;
        private readonly ICacheService _cache;
        private const string CacheKey = "publishers";

        public PublisherService(IPublisherRepository repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            try
            {
                var publishers = _cache.Get<IEnumerable<Publisher>>(CacheKey);
                if (publishers == null)
                {
                    publishers = await _repo.GetAllAsync();
                    _cache.Set(CacheKey, publishers, TimeSpan.FromMinutes(5));
                }
                return publishers;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting cached books", ex);
            }
        }
    }
}
