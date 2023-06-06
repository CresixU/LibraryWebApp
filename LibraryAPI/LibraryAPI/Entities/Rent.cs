namespace LibraryAPI.Entities
{
    public class Rent
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool isDeleted { get; set; } = false;

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public List<Book> Books { get; set; }
    }
}
