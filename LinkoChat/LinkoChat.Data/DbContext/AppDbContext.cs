using LinkoChat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkoChat.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follower>()
                .HasOne(uf => uf.UserFollower)
                .WithMany(u => u.Followees)
                .HasForeignKey(uf => uf.FollowerId);

            modelBuilder.Entity<Follower>()
                .HasOne(uf => uf.UserFollowee)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FolloweeId);



            base.OnModelCreating(modelBuilder);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Follower> UsersFollower { get; set; }
    }
}
