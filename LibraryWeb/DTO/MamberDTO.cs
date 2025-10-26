using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.DTO
{
    public class MamberDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class CreateMemberDTO
    {
        [Required(ErrorMessage ="Name is Required")]
        [Display(Name = "Full Name")]
        [StringLength(25 , MinimumLength = 2 , ErrorMessage = "Your Name Should be between 25 -> 2 Character")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is Invalid Format")]
        [StringLength(50 , ErrorMessage = "Email cant be longer than 50")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }  
    }

    public class UpdateMemberDTO {

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Full Name")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Your Name Should be between 25 -> 2 Character")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is Invalid Format")]
        [StringLength(50, ErrorMessage = "Email cant be longer than 50")]
        public string Email { get; set; }

    }




}
