using LibraryAPI.Models.Books;
using LibraryAPI.Models.Users;

namespace LibraryAPI.Models.Rents
{
    public class RentDTO
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public string User { get; set; }

        public List<BookDTO> Books { get; set; }
    }
}
