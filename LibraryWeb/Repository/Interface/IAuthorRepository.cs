using LibraryWeb.Models;

namespace LibraryWeb.Repository.Interface
{
    public interface IAuthorRepository
    {

        IEnumerable<Author> GetAll();
        Author? GetById(int id);
        Task<Author?> FindByName(string name);
        void Add(Author author);
        void Update(Author author);
        void Delete(int id);
        void Save();

    }
}
