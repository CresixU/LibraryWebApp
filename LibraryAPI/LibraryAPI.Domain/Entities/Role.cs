
namespace LibraryAPI.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Power { get; set; }
        public bool IsImmutable { get; set; }
        public bool isDeleted { get; set; } = false;

        public List<User> Users { get; set; } = new List<User>();
    }
}
