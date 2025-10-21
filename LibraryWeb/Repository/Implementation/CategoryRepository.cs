using LibraryWeb.Data;
using LibraryWeb.Models;
using LibraryWeb.Repository.Interface;

namespace LibraryWeb.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDbContext dbContext;

        public CategoryRepository(LibraryDbContext _dbContext)
        {
            dbContext = _dbContext;
        }



        public void Add(Category category)
        {
            dbContext.Categories.Add(category);
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            if (category != null) {
                dbContext.Categories.Remove(category);
            }
        }

        public IEnumerable<Category> GetAll()
        {
            return dbContext.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            Category? category = dbContext.Categories.FirstOrDefault(c => c.Id == id);
            return category;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
           dbContext.Categories.Update(category);
        }
    }
}
