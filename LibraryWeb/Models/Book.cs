namespace LibraryWeb.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public int CopiesAvailable { get; set; }
        public int TotalCopies { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }

}

