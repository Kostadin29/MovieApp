namespace MovieApp.DataAccess.DataContext
{
    using Microsoft.EntityFrameworkCore;
    using MovieApp.Domain.Models;
    using SEDC.Class05_WorkShop.Models;
    using System;

    public class MovieAppDbContext : DbContext
    {

        public MovieAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // zemame se od OnModelCreating i pisuvame nesto nashe
            base.OnModelCreating(modelBuilder);
            
            // This is relation many to many
            modelBuilder.Entity<Movie>()
                .HasMany(x => x.UserMovies)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.UserMovies)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            // This are assigned values - Ova se zadadeni vrednosti
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(x => x.Age)
                .HasMaxLength(3);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Title) 
                .HasMaxLength(50);

            modelBuilder.Entity<Movie>()
                .Property(x => x.Description)
                .HasMaxLength(250);


        }   

    }
}
