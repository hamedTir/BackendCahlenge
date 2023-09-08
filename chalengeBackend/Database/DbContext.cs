using ChalengeBackend.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace ChalengeBackend.Database
{
    public class ApplicationDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users"); // Set the table name

                entity.HasKey(e => e.Id); // Define the primary key

                // Define column properties using Fluent API
                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.FirstName).HasColumnName("FirstName").HasMaxLength(128).IsRequired();
                entity.Property(e => e.LastName).HasColumnName("LastName").HasMaxLength(128);
                entity.Property(e => e.Email).HasColumnName("Email").IsRequired();
                entity.Property(e => e.Age).HasColumnName("Age").IsRequired();
                entity.Property(e => e.Website).HasColumnName("Website");

                // Define the one-to-many relationship with the Note entity
                entity.HasMany(u => u.Notes)
                    .WithOne(n => n.User)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("Notes"); // Set the table name for the Note entity

                entity.HasKey(e => e.Id); // Define the primary key

                entity.Property(e => e.Id).HasColumnName("Id").IsRequired();
                entity.Property(e => e.Content).HasColumnName("Content").IsRequired().HasColumnType("nvarchar");
                entity.Property(e => e.DateCreated).HasColumnName("DateCreated").IsRequired().HasColumnType("datetime");
                entity.Property(e => e.DateModified).HasColumnName("DateModified").IsRequired().HasColumnType("datetime");
                entity.Property(e => e.Views).HasColumnName("Views").IsRequired();
                entity.Property(e => e.Published).HasColumnName("Published").IsRequired();


            });
        }
    }

}
