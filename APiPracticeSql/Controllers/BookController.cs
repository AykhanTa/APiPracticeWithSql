using ApiPractice.DAL.Data;
using ApiPractice.DAL.Entities;
using APiPracticeSql.Dtos.BookDtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APiPracticeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApiPracticeContext _context;
        private readonly IMapper _mapper;
        public BookController(ApiPracticeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var books=await _context.Books
                .Include(b=>b.BookAuthors)
                .ThenInclude(ba=>ba.Author)
                .ToListAsync();
            return Ok(_mapper.Map<List<BookReturnDto>>(books));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (data == null) return NotFound();
            BookReturnDto bookReturnDto = _mapper.Map<BookReturnDto>(data);
            return Ok(bookReturnDto);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(BookCreateDto bookCreateDto)
        {
            foreach (var authorId in bookCreateDto.AuthorIds)
            {
                if (!await _context.Authors.AnyAsync(a => a.Id == authorId))
                    return BadRequest($"author not found with {authorId}");
            }

            Book book = _mapper.Map<Book>(bookCreateDto);
            foreach (var authorId in bookCreateDto.AuthorIds)
            {
                BookAuthor bookAuthor = new()
                {
                    AuthorId = authorId,
                    BookId=book.Id
                };
                book.BookAuthors.Add(bookAuthor);
            }

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,BookUpdateDto bookUpdateDto)
        {
            foreach (var authorId in bookUpdateDto.AuthorIds)
            {
                if (!await _context.Authors.AnyAsync(a => a.Id == authorId))
                    return BadRequest($"author not found with {authorId}");
            }
            var existBook= await _context.Books
                .Include(b=>b.BookAuthors)
                .ThenInclude(ba=>ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (existBook == null) return NotFound();
            existBook.BookAuthors.Clear();
            foreach (var authorId in bookUpdateDto.AuthorIds)
            {
                BookAuthor bookAuthor = new()
                {
                    AuthorId = authorId,
                    BookId = existBook.Id
                };
                existBook.BookAuthors.Add(bookAuthor);
            }
            _mapper.Map(bookUpdateDto, existBook);
            _context.Update(existBook);
            await _context.SaveChangesAsync();
            return Ok(existBook.Id);
        }

        [HttpDelete("")]
        public async Task<IActionResult> Delete(int id)
        {
            var existBook = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (existBook == null) return NotFound();
            _context.Books.Remove(existBook);
            await _context.SaveChangesAsync();
            return Ok("Succesfully deleted...");

        }
        
    }
}
