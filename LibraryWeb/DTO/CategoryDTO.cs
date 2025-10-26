using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }

    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Category Name is Required")]
        [StringLength(100, MinimumLength =2 , ErrorMessage = "Name Length between 2 -> 100 Charcaters")]
        public string Name { get; set; } = null!;
    }
}
