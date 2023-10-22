using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Application.Models.Roles
{
    public class RoleDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public byte Power { get; set; }
        public bool IsImmutable { get; set; }
    }
}
