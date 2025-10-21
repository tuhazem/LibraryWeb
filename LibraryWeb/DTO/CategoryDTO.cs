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
        [Required]
        public string Name { get; set; } = null!;
    }
}
