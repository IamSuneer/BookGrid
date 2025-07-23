using BookGrid.Models;
using Microsoft.EntityFrameworkCore;

namespace BookGrid.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
            .Property(a => a.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Publisher>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Book>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().HasData(SeedData.Authors);
            modelBuilder.Entity<Publisher>().HasData(SeedData.Publishers);
        }
    }
}
