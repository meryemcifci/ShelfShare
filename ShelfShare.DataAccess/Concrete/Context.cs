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
    public class Context : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

       
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<ReadingGoal> ReadingGoals { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Family relationship
            modelBuilder.Entity<Family>()
                 .HasOne(f => f.Owner)
                 .WithOne(u => u.OwnedFamily)
                 .HasForeignKey<Family>(f => f.OwnerId)
                 .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many relationships
            modelBuilder.Entity<FamilyMember>()
                .HasKey(fm => new { fm.FamilyId, fm.UserId });

            modelBuilder.Entity<UserBook>()
                .HasKey(ub => new { ub.UserId, ub.BookId });

            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            // Book - Family
            modelBuilder.Entity<Book>()
                 .HasOne(b => b.AddedByUser)
                 .WithMany(u => u.AddedBooks) // şimdi tipler uyumlu
                 .HasForeignKey(b => b.AddedByUserId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Restrict);



            // Soft delete filter
            modelBuilder.Entity<AppUser>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<BookCategory>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Family>().HasQueryFilter(f => !f.IsDeleted);
            modelBuilder.Entity<FamilyMember>().HasQueryFilter(fm => !fm.IsDeleted);
            modelBuilder.Entity<ReadingGoal>().HasQueryFilter(rg => !rg.IsDeleted);
            modelBuilder.Entity<Review>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<UserBook>().HasQueryFilter(ub => !ub.IsDeleted);
            base.OnModelCreating(modelBuilder);


        }


    }
}
