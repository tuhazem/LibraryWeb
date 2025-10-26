using LibraryWeb.DTO;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        //private readonly IMemberRepository memberRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public MemberController(IMemberRepository memberRepository , UserManager<ApplicationUser> userManager)
        {

            //this.memberRepository = memberRepository;
            this.userManager = userManager;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllMemebrs()
        {
            var members = await userManager.GetUsersInRoleAsync("Member");
            var result = members.Select(u => new
            {
                u.Id,
                u.FullName,
                u.Email,
            }).ToList();
            return Ok(result);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Member,Admin")]

        public async Task<IActionResult> GetMemberById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await userManager.GetRolesAsync(user);
            if (!roles.Contains("Member")) return NotFound();

            return Ok(new
            {
                user.Id,
                user.FullName,
                user.Email
            });
        }

        

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> CreateMember(CreateMemberDTO dto)
        //{

        //    var Emailexists = await memberRepository.GetByEmail(dto.Email);
        //    if (Emailexists != null) {

        //        return BadRequest("Email Is already Token , Please Enter Antoher Email Address");
        //    }

        //    var member = new Member
        //    {
        //        FullName = dto.FullName,
        //        Email = dto.Email,
        //        PasswordHash = dto.PasswordHash,
        //    };
        //    memberRepository.Add(member);
        //    memberRepository.Save();
        //    return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);

        //}


        //[HttpPut("{id:int}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> UpdateMemberAsync(int id, UpdateMemberDTO dto)
        //{
        //    var member = memberRepository.GetById(id);
        //    if (member == null)
        //    {
        //        return NotFound("Member not found.");
        //    }
        //    var Emailexists = await memberRepository.GetByEmail(dto.Email);
        //    if (Emailexists != null && Emailexists.Id != id)
        //    {
        //        return BadRequest("Email Is already Token , Please Enter Antoher Email Address");
        //    }

        //    member.FullName = dto.FullName;
        //    member.Email = dto.Email;

        //    memberRepository.Update(member);
        //    memberRepository.Save();
        //    return NoContent();

        //}


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await userManager.GetRolesAsync(user);
            if (!roles.Contains("Member")) return BadRequest("User is not a Member");

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { Message = "Member deleted successfully" });
        }


    }
}
