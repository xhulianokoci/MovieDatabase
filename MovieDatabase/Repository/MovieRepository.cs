using MovieDatabase.Data;
using MovieDatabase.Models;
using MovieDatabase.Repository.IRepository;
using System.Linq.Expressions;

namespace MovieDatabase.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private RepositoryDbContext _db;

        public MovieRepository(RepositoryDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Movie obj)
        {
            _db.Movies.Update(obj);
        }
    }
}
