using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.Categories
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
