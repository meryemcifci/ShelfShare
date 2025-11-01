using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShelfShare.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelfShare.DataAccess.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

       
        public DbSet<Book> Books { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<ReadingGoal> ReadingGoals { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reading> Readings { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Group> Group { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔹 User - GroupMember (Many-to-Many gibi ama ara tablo GroupMember ile)
            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.User)
                .WithMany(u => u.GroupMembers)
                .HasForeignKey(gm => gm.UserId);

            modelBuilder.Entity<GroupMember>()
                .HasOne(gm => gm.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(gm => gm.Id);

            // Kullanıcı bir gruba sadece 1 kez katılabilir
            modelBuilder.Entity<GroupMember>()
                .HasIndex(gm => new { gm.GroupId, gm.UserId })
                .IsUnique();
            // 🔹 User - Reading (1 kullanıcının birçok okuması olabilir)
            modelBuilder.Entity<Reading>()
                .HasOne(r => r.User)
                .WithMany(u => u.Readings)
                .HasForeignKey(r => r.UserId);

            // 🔹 Book - Reading (1 kitabı birçok kişi okuyabilir)
            modelBuilder.Entity<Reading>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Readings)
                .HasForeignKey(r => r.BookId);
            modelBuilder.Entity<Reading>()
                .HasOne(r => r.Book)
                .WithMany()
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false); // opsiyonel hale getir

            // 🔹 User - Review (1 kullanıcının birçok yorumu olabilir)
            modelBuilder.Entity<Review>()
                .HasOne(rv => rv.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(rv => rv.UserId);

            // 🔹 Reading - Review (bir okuma -> bir yorum)
            modelBuilder.Entity<Review>()
                .HasOne(rv => rv.Reading)
                .WithMany(r => r.Reviews)   // eğer Reading tarafında ICollection<Review> varsa
                .HasForeignKey(rv => rv.ReadingId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Review - SuggestedToUser (opsiyonel: başka kullanıcıya önerilebilir)
            modelBuilder.Entity<Review>()
                .HasOne(rv => rv.SuggestedToUser)
                .WithMany()
                .HasForeignKey(rv => rv.SuggestedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.ReadingGoals)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);



            // 🔹 User - Notification (1 kullanıcının birçok bildirimi olabilir)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Receiver)
                .WithMany(u => u.ReceivedNotifications)
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Sender)
                .WithMany(u => u.SentNotifications)
                .HasForeignKey(n => n.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Book - Group
           

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Groups)
                .WithMany(g => g.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGroup", // ara tablo adı
                    j => j.HasOne<Group>().WithMany().HasForeignKey("GroupId"),
                    j => j.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                    j =>
                    {
                        j.HasKey("BookId", "GroupId");
                        j.ToTable("BookGroups"); // tablo adı
                    });

            // GroupMember unique (bir user bir gruba 1 kez katılabilir)
            modelBuilder.Entity<GroupMember>()
                .HasIndex(gm => new { gm.Id, gm.UserId })
                .IsUnique();

            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);

            // Soft delete filter
            modelBuilder.Entity<AppUser>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Group>().HasQueryFilter(fm => !fm.IsDeleted);
            modelBuilder.Entity<GroupMember>().HasQueryFilter(fm => !fm.IsDeleted);
            modelBuilder.Entity<Notification>().HasQueryFilter(rg => !rg.IsDeleted);
            modelBuilder.Entity<ReadingGoal>().HasQueryFilter(rg => !rg.IsDeleted);
            modelBuilder.Entity<Review>().HasQueryFilter(r => !r.IsDeleted);
            base.OnModelCreating(modelBuilder);


        }


    }
}
