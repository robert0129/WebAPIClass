using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo1AttributeRouting.Models;
using Demo1AttributeRouting.CustomHandler;

namespace Demo1AttributeRouting.Controllers
{
    [CacheActionAttribute]
    [Produces("application/json")]
    [Route("Books")]
    public class BooksController : Controller
    {
        private readonly NorthWind _context;

        public BooksController(NorthWind context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return _context.Books;
        }

        // GET: api/Books/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> PostBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new {controller = "Books", id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }

        [Route("{*genre}")]
        [HttpGet("{*genre}")]
        public ActionResult GetBookByGenre(string genre)
        {
            var books = _context.Books.Include(b => b.Author)
                .Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));

            return Ok(books);
        }

        [Route("~/authors/{authorId:int}/books")]
        [HttpGet]
        public ActionResult GetBooksByAuthor(int authorId)
        {
            var author = _context.Books.Include(b => b.Author)
                .Where(b => b.AuthorId == authorId);

            return Ok(author);
        }

        [HttpGet]
        [Route("date/{*pubdate:datetime}")]
        //[Route("date/{*pubdate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        public ActionResult Get(DateTime pubdate)
        {
            var books = _context.Books.Include(b => b.Author)
                .Where(b => b.PublishDate == pubdate);

            return Ok(books);
        }
    }
}