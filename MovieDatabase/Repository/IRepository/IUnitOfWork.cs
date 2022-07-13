namespace MovieDatabase.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMovieRepository Movie { get; }
        ISeriesRepository Series { get; }
        void Save();

    }
}
