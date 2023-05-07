namespace LibraryAPI.Models.Roles
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Power { get; set; }
        public bool IsImmutable { get; set; }
    }
}
