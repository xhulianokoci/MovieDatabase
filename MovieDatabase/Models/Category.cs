using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
