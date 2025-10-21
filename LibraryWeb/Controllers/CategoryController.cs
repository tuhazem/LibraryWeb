using LibraryWeb.DTO;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CreateCategory([FromBody] CreateCategoryDTO createCategory) {


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
        public IActionResult Update(int id, [FromBody] CreateCategoryDTO dto) {

            var category = dbcategory.GetById(id);
            if (category == null)
            {
                return BadRequest();
            }
            category.Name = dto.Name;
            dbcategory.Update(category);
            dbcategory.Save();

            return NoContent();


        }


        [HttpDelete("{id:int}")]
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
