using LibraryWeb.Models;

namespace LibraryWeb.Repository.Interface
{
    public interface IBookRepository
    {

        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        Task<Book?> GetByIsbn(string isbn);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
        void Save();

    }
}
