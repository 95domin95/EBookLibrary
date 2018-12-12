using EBookLibraryData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EBookLibraryData
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set;}
        public DbSet<Queue> Queues { get; set; }
        public DbSet<LoanHistory> LoanHistories { get; set; }
        public Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Name = "Science fiction", CategoryId = 1 },
                new Category { Name = "Satyry", CategoryId = 2 },
                new Category { Name = "Horrory", CategoryId = 3 },
                new Category { Name = "Przygodowe", CategoryId = 4 },
                new Category { Name = "Inne", CategoryId = 5 });

            builder.Entity<Copy>().Property(p => p.CopyId).UseSqlServerIdentityColumn();

            builder.Entity<Author>().HasData(
                new Author { Name = "Marian Kowalski", AuthorId = 1 },
                new Author { Name = "Janusz Kwiatkowski", AuthorId = 2 },
                new Author { Name = "Elżbieta Nowak", AuthorId = 3 },
                new Author { Name = "Jerzy Jerzowski", AuthorId = 4 },
                new Author { Name = "Maciej Maciejewski", AuthorId = 5 });

            builder.Entity<Copy>().Property(p => p.CopyId).UseSqlServerIdentityColumn();
        }
    }
}
