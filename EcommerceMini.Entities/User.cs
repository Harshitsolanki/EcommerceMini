using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMini.Entities
{
    [Table("Users")]
    public class User: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        
    }
}
