using LibraryWeb.Data;
using LibraryWeb.DTO;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookrepo;
        private readonly ICategoryRepository catrepo;

        public BookController(IBookRepository _repo, ICategoryRepository categoryRepository)
        {
            bookrepo = _repo;
            this.catrepo = categoryRepository;
        }


        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = bookrepo.GetAll().Select(b => new BookDTO
            {
                Id = b.Id,
                ISBN = b.ISBN,
                Title = b.Title,
                Description = b.Description,
                CopiesAvailable = b.CopiesAvailable,
                TotalCopies = b.TotalCopies,
                CategoryName = b.Category.Name,
                AuthorNames = b.BookAuthors.Select(a => a.Author.FullName).ToList()

            }).ToList();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBookById(int id)
        {
            var book = bookrepo.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            var bookDTO = new BookDTO
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Description = book.Description,
                CopiesAvailable = book.CopiesAvailable,
                TotalCopies = book.TotalCopies,
                CategoryName = book.Category.Name,
                AuthorNames = book.BookAuthors.Select(a => a.Author.FullName).ToList()
            };
            return Ok(bookDTO);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] CreateBookDTO createBook)
        {
            var category = catrepo.GetById(createBook.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid CategoryId");
            }
            var book = new Book
            {
                ISBN = createBook.ISBN,
                Title = createBook.Title,
                Description = createBook.Description,
                CopiesAvailable = createBook.CopiesAvailable,
                TotalCopies = createBook.TotalCopies,
                CategoryId = createBook.CategoryId,
                BookAuthors = createBook.AuthorIds.Select(a => new BookAuthor
                {
                    AuthorId = a
                }).ToList()
            };
            bookrepo.Add(book);
            bookrepo.Save();
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }


        [HttpPut("{id:int}")]
        public IActionResult Update(int id, CreateBookDTO dto) {


            var book = bookrepo.GetById(id);
            if (book == null) { 
                return NotFound();
            }
            book.ISBN = dto.ISBN;
            book.Title = dto.Title;
            book.TotalCopies = dto.TotalCopies;
            book.Description = dto.Description;
            book.CopiesAvailable = dto.CopiesAvailable;
            book.CategoryId = dto.CategoryId;
            book.BookAuthors = dto.AuthorIds.Select(a => new BookAuthor
            {
                AuthorId = a,
                BookId = book.Id
            }).ToList();
            bookrepo.Update(book);
            bookrepo.Save();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id) {

            var book = bookrepo.GetById(id);
            if (book == null) { 
                return NotFound();
            }
            bookrepo.Delete(id);
            bookrepo.Save();
            return NoContent();

        }




    }
}
