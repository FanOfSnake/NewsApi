using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NewsApi.Models.Auth;

namespace NewsApi.Models
{
    public partial class NewsContext: DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get;set; }
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id)
                .IsClustered();

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasDefaultValue("Undefined name");

                entity.Property(e => e.Desc)
                .HasMaxLength(100)
                .HasDefaultValue("Undefined description");

                entity.Ignore(e => e.NewsId);

            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.CurrNewsId, "IX_Comments_CurrNewsId");

                entity.Property(e => e.CurrNewsId)
                .IsRequired();

                entity.Property(e => e.Text)
                .IsRequired()
                .HasDefaultValue("Undefined comment text");

                entity.Property(e => e.TimeWrite)
                .HasDefaultValue<DateTime>(new DateTime (2003,2,17));

                entity.Property(e => e.WriterName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValue("Undefined Writer");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered();

                entity.Property(e => e.Img)
                    .HasMaxLength(100)
                    .IsRequired(false)
                    .HasDefaultValue("Undefined img URL");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasDefaultValue("Undefined name");

                entity.Property(e => e.ShortDesc)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValue("Undefined descripton");

                entity.Property(e => e.Text)    
                    .IsRequired()
                    .HasDefaultValue("Undefined text");

                entity.Property(e => e.TimePublication)
                .IsRequired()
                .HasDefaultValue<DateTime>(new DateTime(2003, 02, 17));

                entity.Ignore(e => e.CategoriesId);
                entity.Ignore(e => e.CommentsId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(true);

                entity.Property(e => e.Login)
                    .HasMaxLength(30)
                    .IsRequired(true)
                    .HasDefaultValue("Undefined Login");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsRequired(true)
                    .HasDefaultValue("Undefined Password");

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsRequired(true)
                    .HasDefaultValue("user");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
