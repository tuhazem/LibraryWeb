using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CopiesAvailable { get; set; }
        public int TotalCopies { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<string> AuthorNames { get; set; } = new();

    }

    public class CreateBookDTO
    {

        [Required(ErrorMessage = "ISBN is Required")]
        [RegularExpression(@"^\d{13}$" , ErrorMessage = "ISBN Must Be 13 Digits")]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        [StringLength(100 , MinimumLength =2 , ErrorMessage = "Title Must be between 2 : 100 characters")]
        public string Title { get; set; } = string.Empty;
        [StringLength(500 , ErrorMessage = "Description Cant be Over 500 Character")]
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Total copies must be at least 1")]
        public int TotalCopies { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Available copies can't be negative")]
        public int CopiesAvailable { get; set; }

        [Required(ErrorMessage = "CategoryId Required")]
        public int CategoryId { get; set; }
        public List<int> AuthorIds { get; set; } = new();
    }
}
