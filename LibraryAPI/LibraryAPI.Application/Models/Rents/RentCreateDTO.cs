
namespace LibraryAPI.Application.Models.Rents
{
    public class RentCreateDTO
    {
        public int UserId { get; set; }
        public DateTime? RentDate { get; set; }
        public int BookId { get; set; }
    }
}
