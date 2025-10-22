namespace LibraryWeb.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

    public class CreateAuthorDTO
    {
        public string FullName { get; set; }
    }

    public class UpdateAuthorDTO
    {
        public string FullName { get; set; }
    }
}
