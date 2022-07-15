using MovieDatabase.Data;
using MovieDatabase.Models;
using MovieDatabase.Repository.IRepository;
using System.Linq.Expressions;

namespace MovieDatabase.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private RepositoryDbContext _db;

        public CategoryRepository(RepositoryDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
