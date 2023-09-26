using LibraryAPI.Entities;
using LibraryAPI.Models.Books;

namespace LibraryAPI.Models.Rents
{
    public class RentCreateDTO
    {
        public int UserId { get; set; }
        public DateTime? RentDate { get; set; }
        public int BookId { get; set; }
    }
}
