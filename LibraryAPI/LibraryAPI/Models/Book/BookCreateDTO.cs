using LibraryAPI.Entities;

namespace LibraryAPI.Models.Book
{
    public class BookCreateDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public int CategoryId { get; set; }
    }
}
