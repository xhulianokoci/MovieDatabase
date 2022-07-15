namespace MovieDatabase.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMovieRepository Movie { get; }
        ISeriesRepository Series { get; }
        IProductRepository Product { get; }

        ICategoryRepository Category { get; }
        void Save();

    }
}
