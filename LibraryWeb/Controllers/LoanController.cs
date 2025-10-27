using LibraryWeb.DTO;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository loanRepo;
        private readonly IBookRepository bookRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public LoanController(ILoanRepository loanRepo, IBookRepository bookRepo, UserManager<ApplicationUser> userManager)
        {
            this.loanRepo = loanRepo;
            this.bookRepo = bookRepo;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllLoans()
        {
            var loans = loanRepo.GetAll().Select(l => new LoanDTO
            {
                Id = l.Id,
                BookTitle = l.Book.Title,
                LoanDate = l.LoanDate,
                ReturnDate = l.ReturnDate
            }).ToList();

            return Ok(loans);
        }

        [HttpGet("My")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> MyLoans() {

            var user = await userManager.GetUserAsync(User);
            if (user == null) {

                return Unauthorized();
            }

            var loans = loanRepo.GetByMember(user.Id)
                .Select(l => new LoanDTO
                { 
                
                    Id = l.Id,
                    BookTitle= l.Book.Title,
                    LoanDate= l.LoanDate,
                    ReturnDate = l.ReturnDate

                });

               return Ok(loans);              
        }


        [HttpPost]
        [Authorize(Roles = "Member,Admin")]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanDTO dto)
        {
            
            var member = await userManager.FindByEmailAsync(dto.MemberEmail);
            if (member == null)
                return BadRequest("Member not found");

            
            var book = bookRepo.GetById(dto.BookId);
            if (book == null)
                return BadRequest("Book not found");

            if (book.CopiesAvailable <= 0)
                return BadRequest("No copies available for this book");

            if (loanRepo.HasActiveLoan(dto.BookId, member.Id))
                return BadRequest("You already borrowed this book and haven't returned it yet.");

            book.CopiesAvailable -= 1;
            bookRepo.Update(book);
            bookRepo.Save();


            var loan = new Loan
            {
                MemberId = member.Id,
                BookId = dto.BookId,
                LoanDate = System.DateTime.Now,
                DueDate = System.DateTime.Now.AddDays(14)
            };

            loanRepo.Add(loan);
            loanRepo.Save();

            var loanDTO = new LoanDTO
            {
                Id = loan.Id,
                MemberFullName = member.FullName,
                MemberEmail = member.Email,
                BookTitle = book.Title,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            };

            return Ok(loanDTO);
        }


        [HttpGet("overdue")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetOverdueLoans()
        {
            var overdueLoans = loanRepo.GetAll()
                .Where(l => !l.IsReturned && l.DueDate < DateTime.Now)
                .Select(l => new LoanDTO
                {
                    Id = l.Id,
                    MemberFullName = l.Member.FullName,
                    MemberEmail = l.Member.Email,
                    BookTitle = l.Book.Title,
                    LoanDate = l.LoanDate,
                    DueDate = l.DueDate
                }).ToList();

            return Ok(overdueLoans);
        }



        [HttpPut("return/{loanId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ReturnLoan(int loanId)
        {
            var loan = loanRepo.GetById(loanId);
            if (loan == null)
                return NotFound("Loan not found");

            if (loan.IsReturned)
                return BadRequest("This book is already returned.");

            //loan.IsReturned = true;
            loan.ReturnDate = DateTime.Now;
            loanRepo.Update(loan);
            loanRepo.Save();

            var book = bookRepo.GetById(loan.BookId);
            if (book != null)
            {
                book.CopiesAvailable += 1;
                bookRepo.Update(book);
                bookRepo.Save();
            }

            return Ok(new
            {
                Message = "Book returned successfully",
                LoanId = loan.Id,
                BookTitle = book?.Title,
                MemberId = loan.MemberId,
                ReturnDate = loan.ReturnDate
            });
        }
    }
}
