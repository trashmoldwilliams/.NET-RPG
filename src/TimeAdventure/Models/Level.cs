using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeAdventure.Models;

namespace TimeAdventure.Models
{
    [Table("Levels")]
    public class Level
    {
        [Key]
        public int LevelId { get; set; }
        public string LevelDescription { get; set; }
        public List<Player> Players { get; internal set; }

        public virtual ICollection <LevelPlayer> LevelPlayer {get; set;}
        public virtual ICollection<Enemy> Enemy { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}