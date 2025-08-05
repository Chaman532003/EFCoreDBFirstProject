using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBFirstProjectEFCore.Models;

public partial class MovieStreamingDbContext : DbContext
{
    public MovieStreamingDbContext()
    {
    }

    public MovieStreamingDbContext(DbContextOptions<MovieStreamingDbContext> options)
        : base(options)
    {
    }

  

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WatchHistory> WatchHistories { get; set; }

   

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385057E5C62E391");

            entity.HasIndex(e => e.Name, "UQ__Genres__737584F69B9FC050").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2941A5C76A668");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PosterUrl).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.VideoUrl).HasMaxLength(255);

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Movies__GenreId__52593CB8");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CEFAD2F66E");

            entity.Property(e => e.ReviewedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Movie).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__Reviews__MovieId__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserId__5AEE82B9");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CF6F58991");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534D3B12841").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("User");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<WatchHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__WatchHis__4D7B4ABD1C53E1A2");

            entity.ToTable("WatchHistory");

            entity.Property(e => e.WatchedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Movie).WithMany(p => p.WatchHistories)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__WatchHist__Movie__571DF1D5");

            entity.HasOne(d => d.User).WithMany(p => p.WatchHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__WatchHist__UserI__5629CD9C");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
