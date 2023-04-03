using LibraryAPI.Entities;
using LibraryAPI.Models.Books;

namespace LibraryAPI.Models.Rents
{
    public class RentCreateDTO
    {
        public DateTime RentDate { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
