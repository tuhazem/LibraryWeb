using LibraryWeb.DTO;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository memberRepository;

        public MemberController(IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
        }


        [HttpGet]
        public IActionResult GetAllMemebrs()
        {
            var members = memberRepository.GetAll();
            var memberDTOs = members.Select(member => new
            {
                Id = member.Id,
                FullName = member.FullName,
                Email = member.Email,

            }).ToList();
            return Ok(memberDTOs);

        }


        [HttpGet("{id:int}")]
        public IActionResult GetMemberById(int id)
        {

            var member = memberRepository.GetById(id);
            if (member == null)
            {
                return NotFound("Member not found.");
            }

            var memberDTO = new MamberDTO
            {
                Id = member.Id,
                FullName = member.FullName,
                Email = member.Email,
            };
            return Ok(memberDTO);

        }

        [HttpPost]
        public IActionResult CreateMember(CreateMemberDTO dto)
        {

            var member = new Member
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
            };
            memberRepository.Add(member);
            memberRepository.Save();
            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);

        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateMember(int id, UpdateMemberDTO dto)
        {
            var member = memberRepository.GetById(id);
            if (member == null)
            {
                return NotFound("Member not found.");
            }
            member.FullName = dto.FullName;
            member.Email = dto.Email;

            memberRepository.Update(member);
            memberRepository.Save();
            return NoContent();

        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteMember(int id)
        {
            var member = memberRepository.GetById(id);
            if (member == null)
            {
                return NotFound("Member not found.");
            }
            memberRepository.Delete(id);
            memberRepository.Save();
            return NoContent();
        }


    }
}
