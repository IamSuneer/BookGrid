using BookGrid.Controllers;
using BookGrid.Interfaces;
using BookGrid.Models;
using BookGrid.Models.DTOS;
using BookGrid.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookGridTest
{
    public class BooksControllerTests
    {
        private Mock<IBookService> _bookServiceMock = null!;
        private BooksController _controller = null!;

        [SetUp]
        public void Setup()
        {
            _bookServiceMock = new Mock<IBookService>();
            _controller = new BooksController(_bookServiceMock.Object);
        }

        [Test]
        public async Task Get_ReturnsOkWithBooks()
        {
            var books = new List<BookViewModel> { new BookViewModel { Id = 1, Title = "Book1", AuthorName = "Author1", PublisherName = "Pub1", Genre = Genre.Fiction } };
            _bookServiceMock.Setup(s => s.GetBooksCachedAsync()).ReturnsAsync(books);

            var result = await _controller.Get();

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(books, okResult.Value);
        }

        [Test]
        public async Task Get_IdNotFound_ReturnsNotFound()
        {
            _bookServiceMock.Setup(s => s.GetBookByIdAsync(It.IsAny<int>())).ReturnsAsync((BookViewModel?)null);

            var result = await _controller.Get(999);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Post_ReturnsCreatedAtAction()
        {
            var dto = new BookDTO { Title = "New Book", AuthorId = 1, PublisherId = 1, Genre = Genre.Fiction };

            var result = await _controller.Post(dto);
            var createdResult = result as OkObjectResult;

            Assert.AreEqual(200, createdResult.StatusCode);
            Assert.AreEqual("Book created successfully", createdResult.Value);
        }

        [Test]
        public async Task Put_BookNotFound_ReturnsNotFound()
        {
            var dto = new BookDTO { Title = "Updated Book", AuthorId = 1, PublisherId = 1, Genre = Genre.Fiction };
            _bookServiceMock.Setup(s => s.UpdateBookAsync(It.IsAny<int>(), dto)).ThrowsAsync(new ArgumentException());

            var result = await _controller.Put(999, dto);
            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual(400, createdResult.StatusCode);
            Assert.AreEqual("Value does not fall within the expected range.", createdResult.Value);
        }
    }
}
