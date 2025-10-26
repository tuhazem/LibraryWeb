using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class CreateAuthorDTO
    {
        [Required(ErrorMessage = "Full name is required.")]
        [Display(Name = "Full Name")]
        [MaxLength(20)]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z\s]+$" , ErrorMessage = "Full name can only contain letters.")]
        public string FullName { get; set; }
    }

    public class UpdateAuthorDTO
    {
        [Required]
        [Display(Name = "Full Name")]
        [MaxLength(20)]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full name can only contain letters.")]
        public string FullName { get; set; }
    }
}
