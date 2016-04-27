using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeAdventure.Models
{
    [Table("LevelPlayers")]
    public class LevelPlayer
    {
        [Key]
        public int LevelPlayerId { get; set; }

        [ForeignKey("Level")]
        public int LevelId { get; set; }

        [ForeignKey("Location")]
        public int PlayerId { get; set; }

        public virtual Level Level { get; set; }
        public virtual Player Player { get; set; }
    }
}
