using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Movie Title")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Movie Description")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Movie Year")]
        public string Year { get; set; }
        
        [Required]
        [DisplayName("Movie Rating")]
        
        public decimal Rating { get; set; } 

        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;
        public DateTime? ModifiedDate { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Range(1,10000)]
        public double ListPrice { get; set; }
        [Required]
        [Range(1,10000)]
        public double Price { get; set; }
        [Required]
        [Range(1,10000)]
        public double Price50 { get; set; }
        [Required]
        [Range(1,10000)]
        public double Price100 { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
