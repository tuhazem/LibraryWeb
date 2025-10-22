using LibraryWeb.Models;
using System.Collections.Generic;

namespace LibraryWeb.Repository.Interface
{
    public interface IMamberRepository
    {

        IEnumerable<Member> GetAll();
        Member? GetById(int id);
        void Add(Member member);
        void Update(Member member);
        void Delete(int id);
        void Save();

    }
}
