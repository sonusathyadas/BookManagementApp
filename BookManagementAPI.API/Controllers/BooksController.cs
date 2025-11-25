using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.Core.Interfaces;
using BookManagementAPI.API.DTOs;
using BookManagementAPI.Core.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagementAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _bookRepository.GetAllBooks();
            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Category cannot be empty");
            }

            var books = await _bookRepository.GetBooksByCategory(category);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }

        [HttpGet("author/{author}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooksByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                return BadRequest("Author cannot be empty");
            }

            var books = await _bookRepository.GetBooksByAuthor(author);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            await _bookRepository.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, _mapper.Map<BookDto>(book));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            if (id != updateBookDto.Id)
            {
                return BadRequest();
            }

            var bookToUpdate = await _bookRepository.GetBookById(id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            var updatedBook = _mapper.Map<Book>(updateBookDto);
            await _bookRepository.UpdateBook(updatedBook);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteBook(id);
            return NoContent();
        }
    }
}