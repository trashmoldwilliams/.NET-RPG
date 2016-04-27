using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using TimeAdventure.Models;

namespace TimeAdventure.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet <Player> Players { get; set; }
        public DbSet <Avatar> Avatars { get; set; }
        public DbSet <LevelPlayer> LevelPlayers { get; set; }
    }
}