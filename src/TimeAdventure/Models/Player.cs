using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeAdventure.Models
{
        [Table("Players")]
        public class Player
        {
            [Key]
            public int PlayerId { get; set; }
            public string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public virtual ICollection <LevelPlayer> LevelPlayers { get; set; }
            public virtual ICollection <Avatar> Avatars { get; set; }
            public virtual ICollection<Enemy> Enemy { get; set; }
            public virtual ApplicationUser User { get; set; }
        }
}
