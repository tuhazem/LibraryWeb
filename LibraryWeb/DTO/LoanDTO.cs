namespace LibraryWeb.DTO
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned => ReturnDate.HasValue;
    }

    public class CreateLoanDTO
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
    }

    public class ReturnLoanDTO
    {
        public int LoanId { get; set; }
    }
}


