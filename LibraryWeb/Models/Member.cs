using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.Models
{
    public class Member
    {

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = null!;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

    }
}
