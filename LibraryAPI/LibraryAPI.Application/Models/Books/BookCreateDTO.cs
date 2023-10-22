using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Application.Models.Books
{
    public class BookCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string Author { get; set; }
        [Range(0, 9999)]
        public int PublicationYear { get; set; }
        public int CategoryId { get; set; }
    }
}
