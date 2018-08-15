using EBookLibraryData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EBookLibraryData
{
    public class Context : IdentityDbContext<Patron>
    {
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookSubject> BookSubjects { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public Context(DbContextOptions options) : base(options) { }
    }
}
