using MovieDatabase.Data;
using MovieDatabase.Models;
using MovieDatabase.Repository.IRepository;
using System.Linq.Expressions;

namespace MovieDatabase.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private RepositoryDbContext _db;

        public ProductRepository(RepositoryDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.Year = obj.Year;
                objFromDb.Rating = obj.Rating;
                objFromDb.ModifiedDate = DateTime.Now.Date;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price = obj.Price;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }

        }
    }
}
