using LibraryWeb.Data;
using LibraryWeb.DTO;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookrepo;
        private readonly ICategoryRepository catrepo;
        private readonly IAuthorRepository author;

        public BookController(IBookRepository _repo, ICategoryRepository categoryRepository , IAuthorRepository author)
        {
            bookrepo = _repo;
            this.catrepo = categoryRepository;
            this.author = author;
        }


        [HttpGet]
        [Authorize(Roles = "Member,Admin")]
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
        [Authorize(Roles = "Member,Admin")]

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDTO createBook)
        {

            var exists = await bookrepo.GetByIsbn(createBook.ISBN);
            if (exists != null)
            {
                return BadRequest("This ISBN IS Already Exists");
            }

            if (createBook.AuthorIds == null || !createBook.AuthorIds.Any())
            {
                return BadRequest("You must provide at least one author ID.");
            }

            var category = catrepo.GetById(createBook.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid CategoryId");
            }

            var authors =  createBook.AuthorIds
                .Select(id => author.GetById(id))
                .ToList();

            if (authors.Any(a => a == null))
            {
                return BadRequest("One or more authors not found.");
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



            var bookDTO = new BookDTO
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Description = book.Description,
                CopiesAvailable = book.CopiesAvailable,
                TotalCopies = book.TotalCopies,
                CategoryName = category.Name,
                AuthorNames = authors.Select(a => a.FullName).ToList()
            };

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, bookDTO);
        }


        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id, CreateBookDTO dto) {


            var book = bookrepo.GetById(id);
            if (book == null) { 
                return NotFound();
            }

            var exists = await bookrepo.GetByIsbn(dto.ISBN);
            if (exists != null && exists.Id != id) {
                return BadRequest("A book with the same ISBN already exists.");
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

            var category = catrepo.GetById(book.CategoryId);
            var bookDTO = new BookDTO
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Description = book.Description,
                CopiesAvailable = book.CopiesAvailable,
                TotalCopies = book.TotalCopies,
                CategoryName = category?.Name ?? "",
                AuthorNames = book.BookAuthors.Select(ba => ba.Author.FullName).ToList()
            };
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
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
