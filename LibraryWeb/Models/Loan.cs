namespace LibraryWeb.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
        public String MemberId { get; set; }
        public ApplicationUser Member { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DateTime DueDate { get; set; }
        public bool IsReturned => ReturnDate.HasValue;
    }
}
