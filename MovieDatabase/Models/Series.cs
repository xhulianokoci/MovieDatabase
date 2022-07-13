using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Models
{
    public class Series
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Movie Title")]
        [Required(ErrorMessage = "Please insert movie title")]
        public string Title { get; set; }
        [DisplayName("Movie Description")]
        [Required(ErrorMessage = "Please insert movie title")]
        public string Description { get; set; }
        [DisplayName("Movie Year")]
        [Required(ErrorMessage = "Please insert movie title")]
        public int Year { get; set; }
        [DisplayName("Movie Gender")]
        [Required(ErrorMessage = "Please insert movie title")]
        public string Gender { get; set; }
        [DisplayName("Movie Rating")]
        [Required(ErrorMessage = "Please insert movie title")]
        public decimal Rating { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string? ImgName { get; set;  }

        public string? ImgPath { get; set; }
        [NotMapped]
        [DisplayName("Movie Image Cover")]
        [Required(ErrorMessage = "Insert movie cover")]
        public IFormFile Image { get; set;  }

        public Series()
        {


        }
        public Series(string title, string description, int year, string gender, decimal rating, DateTime createdDate,  string? imgName, string? imgPath)
        {
            Title = title;
            Description = description;
            Year = year;
            Gender = gender;
            Rating = rating;
            CreatedDate = DateTime.Now.Date;
            ImgName = imgName;
            ImgPath = imgPath;
           
        }
    }
}
