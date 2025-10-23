using Microsoft.AspNetCore.Identity;

namespace LibraryWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
