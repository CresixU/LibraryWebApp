using LibraryAPI.Entities;

namespace LibraryAPI.Models.Books
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }
        public string Category { get; set; }
    }
}
