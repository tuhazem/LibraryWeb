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
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }  
    }

    public class UpdateMemberDTO {

        public string FullName { get; set; }
        public string Email { get; set; }


    }




}
