using LibraryWeb.Data;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Repository.Implementation
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext dbcontext;

        public LoanRepository(LibraryDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public void Add(Loan loan)
        {
            dbcontext.Loans.Add(loan);
        }

        public void Delete(int id)
        {
            var loan = GetById(id);
            if (loan != null)
            {
                dbcontext.Loans.Remove(loan);
            }
        }

        public IEnumerable<Loan> GetAll()
        {
            return dbcontext.Loans.Include(l => l.Book).Include(l => l.Member).AsNoTracking().ToList();
        }

        public Loan? GetById(int id)
        {
            return dbcontext.Loans.Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefault(l=> l.Id == id);
        }

        public IEnumerable<Loan> GetByMember(int memberId)
        {
            return dbcontext.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.MemberId == memberId)
                .AsNoTracking()
                .ToList();
        }

        public bool HasActiveLoan(int bookId, int memberId)
        {
            return dbcontext.Loans.Any(l => l.BookId == bookId && l.MemberId == memberId && !l.IsReturned);
        }

        public void Save()
        {
            dbcontext.SaveChanges();
        }

        public void Update(Loan loan)
        {
            dbcontext.Loans.Update(loan);
        }
    }
}
