using LibraryWeb.Models;

namespace LibraryWeb.Repository.Interface
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        Member? GetById(int id);
        Member? GetByEmail(string email);
        void Add(Member member);
        void Update(Member member);
        void Delete(int id);
        void Save();

    }
}
