using LibraryWeb.Models;

namespace LibraryWeb.Repository.Interface
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetAll();
        Loan? GetById(int id);
        IEnumerable<Loan> GetByMember(int memberId);
        void Add(Loan loan);
        void Update(Loan loan);
        void Delete(int id);
        void Save();

        // optional helper to check existing active loan for same book & member
        bool HasActiveLoan(int bookId, int memberId);
    }
}
