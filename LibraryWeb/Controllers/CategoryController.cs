using LibraryWeb.DTO;
using LibraryWeb.Models;
using LibraryWeb.Repository.Implementation;
using LibraryWeb.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository dbcategory;

        public CategoryController(ICategoryRepository category)
        {
            this.dbcategory = category;
        }

        [HttpGet]
        [Authorize(Roles = "Member,Admin")]
        public IActionResult GetAllCategories()
        {
            var categories = dbcategory.GetAll().Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Member,Admin")]
        public IActionResult GetCategoryById(int id)
        {
            var category = dbcategory.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
            return Ok(categoryDTO);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO createCategory) {

            
            var exist = await dbcategory.GetByName(createCategory.Name);
            if (exist != null) {
                return BadRequest("Category Name is already Exist");
            }

            var category = new Category();
            category.Name = createCategory.Name;
            dbcategory.Add(category);
            dbcategory.Save();
            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
                
            };
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDTO);

        }


        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCategoryDTO dto) {

            var category = dbcategory.GetById(id);
            if (category == null)
            {
                return BadRequest();
            }

            var exist = await dbcategory.GetByName(dto.Name);
            if (exist != null && exist.Id != id) {
                return BadRequest("Category Name is already Exist");
            }

            category.Name = dto.Name;
            dbcategory.Update(category);
            dbcategory.Save();

            return NoContent();


        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id) {

            var category = dbcategory.GetById(id);
            if (category == null) {
                return NotFound();
            }
            dbcategory.Delete(id);
            dbcategory.Save();
            return NoContent();
        
        }






    }

}
