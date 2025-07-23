using BookGrid.DAL.Repositories;
using BookGrid.Interfaces;
using BookGrid.Models;
using BookGrid.Models.DTOS;
using BookGrid.Models.ViewModels;
using BookGrid.Services;
using Moq;

namespace BookGridTest
{
    public class BookServiceTests
    {
        private Mock<IBookRepository> _repoMock = null!;
        private Mock<ICacheService> _cacheMock = null!;
        private BookService _service = null!;

        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IBookRepository>();
            _cacheMock = new Mock<ICacheService>();

            _service = new BookService(_repoMock.Object, _cacheMock.Object);
        }

        [Test]
        public async Task GetBooksCachedAsync_WhenCacheIsEmpty_ShouldFetchFromRepoAndCache()
        {
            var books = new List<BookViewModel>
            {
                new() { Id = 1, Title = "Book 1", AuthorName = "Author A", PublisherName = "Pub X", Genre = Genre.Fiction }
            };

            _cacheMock.Setup(c => c.Get<IEnumerable<BookViewModel>>(It.IsAny<string>())).Returns((IEnumerable<BookViewModel>?)null);
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

            var result = await _service.GetBooksCachedAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<BookViewModel>)result).Count);
            _cacheMock.Verify(c => c.Set(
                It.IsAny<string>(),
                It.IsAny<IEnumerable<BookViewModel>>(),
                It.IsAny<TimeSpan>()), Times.Once);
        }

        [Test]
        public async Task AddBookAsync_ShouldAddBookAndRemoveCache()
        {
            var dto = new BookDTO { Title = "New Book", AuthorId = 1, PublisherId = 1, Genre = Genre.Fantasy };

            await _service.AddBookAsync(dto);

            _repoMock.Verify(r => r.AddAsync(It.IsAny<BookDTO>()), Times.Once);
            _cacheMock.Verify(c => c.Remove(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task UpdateBookAsync_WhenBookNotFound_ShouldThrowException()
        {
            _repoMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>())).ReturnsAsync((Book?)null);

            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.UpdateBookAsync(99, new BookDTO { Title = "x", AuthorId = 1, PublisherId = 1, Genre = Genre.Fiction }));

            Assert.That(ex.Message, Is.EqualTo("Error updating book"));
            Assert.That(ex.InnerException, Is.TypeOf<ArgumentException>());
            Assert.That(ex.InnerException.Message, Is.EqualTo("Book not found"));
        }
    }
}