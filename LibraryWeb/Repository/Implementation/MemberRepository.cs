using LibraryWeb.Data;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;

namespace LibraryWeb.Repository.Implementation
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext dbcontext;

        public MemberRepository(LibraryDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public void Add(Member member)
        {
            dbcontext.Members.Add(member);
        }

        public void Delete(int id)
        {
         var member = GetById(id);
            if (member != null)
            {
                dbcontext.Members.Remove(member);
            }
        }

        public IEnumerable<Member> GetAll()
        {
            return dbcontext.Members.ToList();
        }

        public Member? GetByEmail(string email)
        {
            return dbcontext.Members.FirstOrDefault(m => m.Email == email);
        }

        public Member? GetById(int id)
        {
            return dbcontext.Members.FirstOrDefault(m => m.Id == id);
        }

        public void Save()
        {
            dbcontext.SaveChanges();
        }

        public void Update(Member member)
        {
            dbcontext.Members.Update(member);
        }
    }
}
