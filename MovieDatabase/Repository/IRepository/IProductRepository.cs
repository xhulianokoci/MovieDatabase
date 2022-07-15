using MovieDatabase.Models;

namespace MovieDatabase.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);

    }
}
