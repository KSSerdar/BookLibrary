using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class APIContext:IdentityDbContext<User>
    {
        public APIContext(DbContextOptions<APIContext>options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasOne(c => c.Author).WithMany(c => c.Books).HasForeignKey(c => c.AuthorID);
            modelBuilder.Entity<Comment>().HasOne(d=>d.User).WithMany(d=>d.Replies).HasForeignKey(d => d.UserID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().HasOne(d=>d.Book).WithMany(d=>d.Replies).HasForeignKey(d => d.BookID).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
       // public DbSet<User> Users { get; set; }
        public DbSet<Comment> Replies { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

    }
}
