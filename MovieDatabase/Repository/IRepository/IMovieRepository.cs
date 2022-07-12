using MovieDatabase.Models;

namespace MovieDatabase.Repository.IRepository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie obj);

        void Save();

        
    }
}
