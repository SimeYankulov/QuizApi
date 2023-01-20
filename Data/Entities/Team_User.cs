using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Team_Users")]
    public class Team_User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TeamId { get; set; } = default!;
        public Team? team { get; set; } = default!; 
        public int UserId { get; set; } = default!;
        public User? user { get; set; } = default!;

    }
}
