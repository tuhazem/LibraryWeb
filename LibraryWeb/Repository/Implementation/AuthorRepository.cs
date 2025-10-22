using LibraryWeb.Data;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;

namespace LibraryWeb.Repository.Implementation
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext dbcontext;

        public AuthorRepository(LibraryDbContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public void Add(Author author)
        {
            dbcontext.Authors.Add(author);
        }

        public void Delete(int id)
        {
            var author = GetById(id);
            if (author != null)
            {
                dbcontext.Authors.Remove(author);
            }
        }

        public IEnumerable<Author> GetAll()
        {
            return dbcontext.Authors.ToList();
        }

        public Author? GetById(int id)
        {
            return dbcontext.Authors.FirstOrDefault(a=>a.Id == id);
        }

        public void Save()
        {
            dbcontext.SaveChanges();
        }

        public void Update(Author author)
        {
            dbcontext.Authors.Update(author);
        }
    }   
}
