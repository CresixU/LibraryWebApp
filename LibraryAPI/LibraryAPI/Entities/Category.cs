namespace LibraryAPI.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isDeleted { get; set; } = false;

        public List<Book> Books { get; set; }
    }
}
