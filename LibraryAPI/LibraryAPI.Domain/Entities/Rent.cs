namespace LibraryAPI.Domain.Entities
{
    public class Rent
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool isDeleted { get; set; } = false;

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
