using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.DTO
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public string MemberFullName { get; set; }
        public string MemberEmail { get; set; }
        public string BookTitle { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class CreateLoanDTO
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public string MemberEmail { get; set; }
    }

    public class ReturnLoanDTO
    {
        [Required]
        public int LoanId { get; set; }
    }
}


