using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeAdventure.Models
{
    [Table("Enemies")]
    public class Enemy
    {
        [Key]
        public int EnemyId { get; set; }
        public string EnemyDescription { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int LevelId { get; set; }
        public virtual Level Level { get; set; }
    }
}