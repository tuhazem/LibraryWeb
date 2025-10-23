using LibraryWeb.DTO;
using LibraryWeb.Models;

namespace LibraryWeb.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(ApplicationUser user);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO);
    }
}
