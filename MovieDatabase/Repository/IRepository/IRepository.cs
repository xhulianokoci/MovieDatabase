using System.Linq.Expressions;

namespace MovieDatabase.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //Get First Or Default
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        //Get All Objects
        IEnumerable<T> GetAll();
        //Add an object
        void Add(T entity);
        //Remove an object
        void Remove(T entity);
        //Remove multiple objects
        void RemoveRange(IEnumerable<T> entities);
        //AddAsync Method
        void AddAsync(T entity);
        //Find object by ID
        T Find(int? id);
        //Clear database index
        void Clear();

    }
}
