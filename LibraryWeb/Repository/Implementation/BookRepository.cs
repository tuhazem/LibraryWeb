using LibraryWeb.Data;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext dblibrary;

        public BookRepository(LibraryDbContext libraryDb)
        {
            this.dblibrary = libraryDb;
        }

        public void Add(Book book)
        {
            dblibrary.Books.Add(book);
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null) { 
                dblibrary.Books.Remove(book);
            }
        }

        public IEnumerable<Book> GetAll()
        {
            return dblibrary.Books.Include(c => c.Category)
                .Include(a => a.BookAuthors).ThenInclude(a => a.Author)
                .AsNoTracking()
                .ToList();
        }

        public Book? GetById(int id)
        {
            return dblibrary.Books.Include(c=>c.Category)
                .Include(a=> a.BookAuthors).ThenInclude(a=>a.Author)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Save()
        {
            dblibrary.SaveChanges();
        }

        public void Update(Book book)
        {
            dblibrary.Books.Update(book);
        }
    }
}
