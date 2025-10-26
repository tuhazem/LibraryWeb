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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Member,Admin")]
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
        [Authorize(Roles = "Member,Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAuthor(CreateAuthorDTO dto)
        {

            var existingAuthor = await authorRepository.FindByName(dto.FullName);
            if (existingAuthor != null)
            {
                return BadRequest("Author with the same name already exists.");
            }

            var author = new Author
            {
                FullName = dto.FullName,
            };
            authorRepository.Add(author);
            authorRepository.Save();
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, dto);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDTO dto)
        {
            var author = authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound("Author not found.");
            }

            var exist = await authorRepository.FindByName(dto.FullName);
            if (exist != null && exist.Id != id) {
                return BadRequest("Author with the same name already exists.");
            }

            author.FullName = dto.FullName;
            authorRepository.Update(author);
            authorRepository.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
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

