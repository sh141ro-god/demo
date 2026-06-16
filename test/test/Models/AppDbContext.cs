using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Copy> Copies { get; set; }

    public virtual DbSet<Edition> Editions { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=library;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasDefaultValue("");

            entity.HasMany(d => d.IdAuthors).WithMany(p => p.IdBooks)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorsBook",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("IdAuthor")
                        .HasConstraintName("FK_AuthorsBook_Author"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("IdBook")
                        .HasConstraintName("FK_AuthorsBook_Book"),
                    j =>
                    {
                        j.HasKey("IdBook", "IdAuthor").HasName("PK_AuthorsBook");
                        j.ToTable("Authors_Book");
                        j.IndexerProperty<int>("IdBook").HasColumnName("Id_Book");
                        j.IndexerProperty<int>("IdAuthor").HasColumnName("Id_Author");
                    });

            entity.HasMany(d => d.IdCategories).WithMany(p => p.IdBooks)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("IdCategory")
                        .HasConstraintName("FK_BookCategories_Category"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("IdBook")
                        .HasConstraintName("FK_BookCategories_Book"),
                    j =>
                    {
                        j.HasKey("IdBook", "IdCategory").HasName("PK_BookCategories");
                        j.ToTable("Book_Categories");
                        j.IndexerProperty<int>("IdBook").HasColumnName("Id_Book");
                        j.IndexerProperty<int>("IdCategory").HasColumnName("Id_Category");
                    });

            entity.HasMany(d => d.IdGenres).WithMany(p => p.IdBooks)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("IdGenre")
                        .HasConstraintName("FK_BookGenres_Genre"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("IdBook")
                        .HasConstraintName("FK_BookGenres_Book"),
                    j =>
                    {
                        j.HasKey("IdBook", "IdGenre").HasName("PK_BookGenres");
                        j.ToTable("Book_Genres");
                        j.IndexerProperty<int>("IdBook").HasColumnName("Id_Book");
                        j.IndexerProperty<int>("IdGenre").HasColumnName("Id_Genre");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasDefaultValue("");
        });

        modelBuilder.Entity<Copy>(entity =>
        {
            entity.HasOne(d => d.IdConditionNavigation).WithMany(p => p.Copies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Copies_Condition");

            entity.HasOne(d => d.IdEditionNavigation).WithMany(p => p.Copies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Copies_Edition");
        });

        modelBuilder.Entity<Edition>(entity =>
        {
            entity.Property(e => e.Name).HasDefaultValue("");
            entity.Property(e => e.YearOfPublication).HasDefaultValueSql("(datepart(year,getdate()))");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Editions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Edition_Book");

            entity.HasOne(d => d.IdPublishingHouseNavigation).WithMany(p => p.Editions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Edition_PublishingHouse");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Inn).HasDefaultValue("");
            entity.Property(e => e.Number).HasDefaultValue("");
            entity.Property(e => e.PassportNumber).HasDefaultValue("");

            entity.HasOne(d => d.IdJobNavigation).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Job");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasDefaultValue("");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.Property(e => e.DateOfIssue).HasDefaultValueSql("(CONVERT([date],getdate()))");

            entity.HasOne(d => d.IdCopyNavigation).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_Copy");

            entity.HasOne(d => d.IdReaderNavigation).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_Reader");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Log_Status");
        });

        modelBuilder.Entity<PublishingHouse>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.Property(e => e.Number).HasDefaultValue("");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
