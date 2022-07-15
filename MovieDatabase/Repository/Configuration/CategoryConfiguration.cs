using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieDatabase.Models;

namespace Repository.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
           builder.HasData(
        new Category
        {
            Id = 1,
            Code = "act",
            Description = "Action",
        },
        new Category
        {
            Id = 2,
            Code = "com",
            Description = "Comedy",
        },
        new Category
        {
            Id = 3,
            Code = "dra",
            Description = "Drama",
        },
        new Category
        {
            Id = 4,
            Code = "fan",
            Description = "Fantasy",
        },
        new Category
        {
            Id = 5,
            Code = "hor",
            Description = "Horror",
        },
        new Category
        {
            Id = 6,
            Code = "mys",
            Description = "Mystery",
        },
        new Category
        {
            Id = 7,
            Code = "rom",
            Description = "Romace",
        },
        new Category
        {
            Id = 8,
            Code = "thri",
            Description = "Thriller",
        },
        new Category
        {
            Id = 9,
            Code = "cri",
            Description = "Crime",
        },
        new Category
        {
            Id = 10,
            Code = "adv",
            Description = "Adventure",
        },
        new Category
        {
            Id = 11,
            Code = "sfi",
            Description = "Sci-Fi",
        }
        );
        }
    }
}
