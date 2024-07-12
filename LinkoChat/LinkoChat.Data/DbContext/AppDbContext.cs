using LinkoChat.Data.Properties;
using LinkoChat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Text;
using System.Text.Json;

namespace LinkoChat.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<State> Statements { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Domain.Models.Queue> Queues { get; set; }

    }
}
