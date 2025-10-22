using LibraryWeb.Data;
using LibraryWeb.DTO;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository loanRepository;
        private readonly IBookRepository bookRepository;
        private readonly IMemberRepository memberRepository;
        private readonly LibraryDbContext dbContext;

        public LoanController(
            ILoanRepository loanRepository,
            IBookRepository bookRepository,
            IMemberRepository memberRepository,
            LibraryDbContext dbContext
            )
        {
            this.loanRepository = loanRepository;
            this.bookRepository = bookRepository;
            this.memberRepository = memberRepository;
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllLoans()
        {
            var loans = loanRepository.GetAll();
            var LoanDTOs = loans.Select(loan => new LoanDTO
            {
                Id = loan.Id,
                BookId = loan.BookId,
                BookTitle = loan.Book.Title,
                MemberId = loan.MemberId,
                MemberName = loan.Member.FullName,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            }).ToList();

            return Ok(LoanDTOs);
        }

        [HttpGet("member/{memberId:int}")]
        public IActionResult GetByMember(int memberId) { 
        
            var loans = loanRepository.GetByMember(memberId);
            if (loans == null || !loans.Any()) {
                return NotFound("No loans found for the specified member.");
            }
            var LoanDTOs = loans.Select(loan => new LoanDTO
            {
                Id = loan.Id,
                BookId = loan.BookId,
                BookTitle = loan.Book.Title,
                MemberId = loan.MemberId,
                MemberName = loan.Member.FullName,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            }).ToList();
            return Ok(LoanDTOs);

        }

        [HttpPost("borrow")]
        public IActionResult CreateLoan([FromBody] CreateLoanDTO createLoanDTO)
        {
            var book = bookRepository.GetById(createLoanDTO.BookId);
            if (book == null || book.CopiesAvailable <= 0)
            {
                return BadRequest("Book is not available for loan.");
            }
            var member = memberRepository.GetById(createLoanDTO.MemberId);
            if (member == null)
            {
                return BadRequest("Member not found.");
            }
            var loan = new Loan
            {
                BookId = createLoanDTO.BookId,
                MemberId = createLoanDTO.MemberId,
                LoanDate = DateTime.UtcNow
            };

            loanRepository.Add(loan);
            book.CopiesAvailable--;
            bookRepository.Update(book);
            loanRepository.Save();
            return Ok();



        }


        [HttpPost("return")]
        public IActionResult ReturnLoan([FromBody] ReturnLoanDTO returnLoanDTO)
        {
            var loan = loanRepository.GetById(returnLoanDTO.LoanId);
            if (loan == null)
            {
                return NotFound("Loan not found.");
            }

            if (loan.ReturnDate != null)
            {
                return BadRequest("This loan has already been returned.");
            }

            var book = bookRepository.GetById(loan.BookId);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            loan.ReturnDate = DateTime.UtcNow;
            loanRepository.Update(loan);
            book.CopiesAvailable++;
            bookRepository.Update(book);
            loanRepository.Save();
            return Ok();
        }
        }
}
