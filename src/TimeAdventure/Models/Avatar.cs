using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeAdventure.Models
{
    [Table("Avatars")]
    public class Avatar
    {
        [Key]
        public int AvatarId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
