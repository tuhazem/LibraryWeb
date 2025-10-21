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
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int TotalCopies { get; set; }
        public int CopiesAvailable { get; set; }

        public int CategoryId { get; set; }
        public List<int> AuthorIds { get; set; } = new();
    }
}
