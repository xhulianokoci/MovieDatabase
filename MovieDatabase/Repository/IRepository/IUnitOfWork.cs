namespace MovieDatabase.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMovieRepository Movie { get; }

        void Save();

    }
}
