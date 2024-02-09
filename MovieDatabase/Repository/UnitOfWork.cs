using MovieDatabase.Data;
using MovieDatabase.Repository.IRepository;

namespace MovieDatabase.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IMovieRepository Movie {get; private set;}
        public ISeriesRepository Series {get; private set;}
        public IProductRepository Product {get; private set;}
        public ICategoryRepository Category {get; private set;}
        
        private RepositoryDbContext _db;

        public UnitOfWork(RepositoryDbContext db)
        {
            _db = db;
            Movie = new MovieRepository(_db);
            Series = new SeriesRepository(_db);
            Product = new ProductRepository(_db);
            Category = new CategoryRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
