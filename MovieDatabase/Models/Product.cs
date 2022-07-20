using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please insert product title")]
        [DisplayName("Product Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please insert product description")]
        [DisplayName("Product Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please insert product year")]
        [DisplayName("Product Year")]
        public string Year { get; set; }
        
        [Required(ErrorMessage = "Please insert product rating")]
        [DisplayName("Product Rating")]
        public decimal Rating { get; set; } 

        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;
        public DateTime? ModifiedDate { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Please insert product list price")]
        [Range(1,10000)]
        public double ListPrice { get; set; }
        [Required(ErrorMessage = "Please insert product price")]
        [Range(1,10000)]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please insert product price for 50")]
        [Range(1,10000)]
        public double Price50 { get; set; }
        [Required(ErrorMessage = "Please insert product price for 100")]
        [Range(1,10000)]
        public double Price100 { get; set; }
        [Required(ErrorMessage = "Please insert product category")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
