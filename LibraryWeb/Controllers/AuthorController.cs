using LibraryWeb.DTO;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var authors = authorRepository.GetAll();
            var authorDTOs = authors.Select(author => new
            {
                Id = author.Id,
                FullName = author.FullName,
            }).ToList();
            return Ok(authorDTOs);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) { 
        
            var author = authorRepository.GetById(id);
            if (author == null) {
                return NotFound("Author not found.");
            }

            AuthorDTO authorDTO = new AuthorDTO
            {
                Id = author.Id,
                FullName = author.FullName,
            };
             return Ok(authorDTO);
        
        }

        [HttpPost]
        public IActionResult CreateAuthor(CreateAuthorDTO dto)
        {
            var author = new Author
            {
                FullName = dto.FullName,
            };
            authorRepository.Add(author);
            authorRepository.Save();
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, dto);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAuthor(int id, UpdateAuthorDTO dto)
        {
            var author = authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }
            author.FullName = dto.FullName;
            authorRepository.Update(author);
            authorRepository.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }
            authorRepository.Delete(id);
            authorRepository.Save();
            return NoContent();
        }

    }
}

