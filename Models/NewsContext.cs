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
                entity.HasData(
                    new Category { Id = 1, Name = "IT", Desc = "Краткое описание категории" },
                    new Category { Id = 2, Name = "Политика", Desc = "Краткое описание категории" },
                    new Category { Id = 3, Name = "Книги", Desc = "Краткое описание категории" },
                    new Category { Id = 4, Name = "Музыка", Desc = "Краткое описание категории" }
                    );

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
                entity.HasData(
                    new Comment { Id=1, WriterName = "Рамиль", Text = "Молодец! Продолжай в том же духе!", TimeWrite = DateTime.Now, CurrNewsId = 1 },
                    new Comment { Id = 2, WriterName = "Шаукат", Text = "Спасибо! Очень приятно такое читать)", TimeWrite = DateTime.Now + TimeSpan.FromMinutes(5), CurrNewsId = 1 }
                    );

                entity.HasIndex(e => e.CurrNewsId, "IX_Comments_CurrNewsId");

                entity.Property(e => e.CurrNewsId)
                .IsRequired().HasDefaultValue(1);

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
                entity.HasData(
                    new News { Id = 1, Name = "Шаукат входит в IT-сферу!", ShortDesc = "Краткое описание новости", TimePublication = DateTime.Now },
                    new News { Id = 2, Name = "Книги о музыке снова популярны!", ShortDesc = "Краткое описание новости", TimePublication = DateTime.Now + TimeSpan.FromDays(1) },
                    new News { Id = 3, Name = "Навальный использует хакеров для развращения интернета!", ShortDesc = "Краткое описание новости", TimePublication = DateTime.Now + TimeSpan.FromDays(2) },
                    new News { Id = 4, Name = "Русские шпионы вторгаются в выборы США!", ShortDesc = "Краткое описание новости", TimePublication = DateTime.Now + TimeSpan.FromDays(3) }
                    );

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
                entity.HasData(
                    new User { Id= 1, Login = "shaukatk@list.ru", Password = "ueptkm1933", Role = "admin" },
                    new User { Id= 2, Login = "admin", Password = "admin", Role = "admin" },
                    new User { Id= 3, Login = "user", Password = "user", Role = "user" }
                    );

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
