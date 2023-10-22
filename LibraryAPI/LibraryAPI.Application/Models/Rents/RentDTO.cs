

namespace LibraryAPI.Application.Models.Rents
{
    public class RentDTO
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public string User { get; set; }

        public int BookId { get; set; }
    }
}
