using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Movie Title")]
        [Required(ErrorMessage = "Please insert movie title")]
        public string Title { get; set; }

        [DisplayName("Movie Description")]
        [Required(ErrorMessage = "Please insert movie description")]
        public string Description { get; set; }

        [DisplayName("Movie Production Year")]
        [Required(ErrorMessage = "Please insert movie year")]
        public int Year { get; set; }

        [DisplayName("Movie Rating")]
        [Required(ErrorMessage = "Please inser movie rating")]
        public decimal Rating { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;

        public DateTime? ModifiedDate { get; set; }

        public string? ImgName { get; set; }

        public string? ImgPath { get; set; }

        [NotMapped]
        [DisplayName("Movie Image Cover")]
        [Required(ErrorMessage = "Insert movie cover")]
        public IFormFile Image { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public Movie()
        {

        }

        public Movie(string title, string description, int year, decimal rating, string imgName, string imgPath, int categoryId)
        {
            Title = title;
            Description = description;
            Year = year;
            Rating = rating;
            CreatedDate = DateTime.Now.Date;
            ImgName = imgName;
            ImgPath = imgPath;
            CategoryId = categoryId;
        }
    }
}
