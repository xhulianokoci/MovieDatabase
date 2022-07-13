using MovieDatabase.Models;

namespace MovieDatabase.Repository.IRepository
{
    public interface ISeriesRepository : IRepository<Series>
    {
        void Update(Series obj);

    }
}
