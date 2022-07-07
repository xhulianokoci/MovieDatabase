using Microsoft.EntityFrameworkCore;
using MovieDatabase.Models;

namespace MovieDatabase.Data
{
    public class RepositoryDbContext : DbContext
    {

        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
