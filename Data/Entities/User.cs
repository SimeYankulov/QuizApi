using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; } = default!;
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public byte[]? Salt { get; set; } = default!;
        public Role Role { get; set; } = default!;   
        public int RoleId { get; set; } = default!;
        public ICollection<Team_User>? teamUsers { get; set; }
    }
}
