using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Teams")]
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; } 
        public string Name { get; set; } = default!;
        public string Captain_Name { get; set; } = default!;        
        public int Points { get; set; } = default!;
        public ICollection<Team_User> team_Users { get; set; } = default!;
    }
}
