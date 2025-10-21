namespace LibraryWeb.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
