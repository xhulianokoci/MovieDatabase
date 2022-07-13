using MovieDatabase.Data;
using MovieDatabase.Models;
using MovieDatabase.Repository.IRepository;
using System.Linq.Expressions;

namespace MovieDatabase.Repository
{
    public class SeriesRepository : Repository<Series>, ISeriesRepository
    {
        private RepositoryDbContext _db;

        public SeriesRepository(RepositoryDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Series obj)
        {
            _db.Series.Update(obj);
        }
    }
}
