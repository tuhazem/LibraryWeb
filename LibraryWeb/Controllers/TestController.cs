using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult GetPublic()
        {
            return Ok(new { message = "This is a public endpoint" });
        }

        [HttpGet("private")]
        [Authorize]
        public IActionResult GetPrivate()
        {
            var userEmail = User.Identity?.Name;
            var userId = User.FindFirst("sub")?.Value;
            var userFullName = User.FindFirst("unique_name")?.Value;

            return Ok(new 
            { 
                message = "This is a protected endpoint",
                user = new
                {
                    email = userEmail,
                    id = userId,
                    fullName = userFullName
                }
            });
        }
    }
}
