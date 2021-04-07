using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NewsApi.Models
{
    public partial class NewsContext: DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        //public DbSet<CategoryNews> CategoryNews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=myNewsDB;Trusted_Connection=True;");

        public NewsContext () => Database.EnsureCreated();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Desc).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            
            /*modelBuilder.Entity<CategoryNews>(entity =>
            //{
            //    entity.HasKey(e => new { e.CategoriesId, e.NewsId });

            //    entity.HasIndex(e => e.NewsId, "IX_CategoryNews_NewsId");

            //});*/

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.CurrNewsId, "IX_Comments_CurrNewsId");

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.WriterName)
                    .IsRequired()
                    .HasMaxLength(30);

                //entity.HasOne(d => d.CurrNews)
                //    .WithMany(p => p.Comments)
                //    .HasForeignKey(d => d.CurrNewsId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Img).HasMaxLength(100);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ShortDesc)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Text).IsRequired();
            });

            //modelBuilder.Entity<CategoryNews>().ToTable("CategoryNews");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
