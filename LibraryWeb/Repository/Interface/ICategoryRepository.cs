using LibraryWeb.Models;

namespace LibraryWeb.Repository.Interface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();

        Category? GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
        void Save();

        


    }
}
