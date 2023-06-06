namespace LibraryAPI.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }
        public bool isDeleted { get; set; } = false;

        public List<Rent> Rents { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
