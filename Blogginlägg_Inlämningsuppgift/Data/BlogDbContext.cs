using Blogginlägg_Inlämningsuppgift.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogginlägg_Inlämningsuppgift.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; } 

        public virtual DbSet<Post> Posts { get; set; } 

        public virtual DbSet<Comment> Comments { get; set; } 

        public virtual DbSet<Category> Categories { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}