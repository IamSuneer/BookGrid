using BookGrid.Interfaces;
using BookGrid.Models;
using BookGrid.Models.DTOS;
using BookGrid.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookGrid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        // Explicit constructor
        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var books = await _service.GetBooksCachedAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var book = await _service.GetBookByIdAsync(id);
                return book != null ? Ok(book) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookDTO dto)
        {
            try
            {
                await _service.AddBookAsync(dto);
                await _service.SaveAsync();
                return Ok("Book created successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookDTO dto)
        {
            try
            {
                await _service.UpdateBookAsync(id, dto);
                await _service.SaveAsync();
                return Ok(new { Message = "Book updated successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<BookDTO> dto)
        {
            try
            {
                var book = await _service.GetBookDtoByIdAsync(id);
                if (book == null) return NotFound();

                TryValidateModel(book);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _service.UpdateBookAsync(id, book);
                await _service.SaveAsync();
                return Ok(new { Message = "Book patched successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
