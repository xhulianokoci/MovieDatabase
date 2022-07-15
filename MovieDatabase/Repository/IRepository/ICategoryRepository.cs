using MovieDatabase.Models;

namespace MovieDatabase.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);

    }
}
