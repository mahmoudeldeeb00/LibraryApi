using Api_Project.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Project.DAL.DataBase
{
    public class DbContainer : IdentityDbContext<LibraryUser>
    {
        public DbContainer(DbContextOptions<DbContainer> opts) : base(opts) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Library> Libraries { get; set; }
        
        public DbSet<BookLibrary> BookLibraries { get; set; }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<LibraryBookChecked> LibraryBooksChecked { get; set; }


        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
     





        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Author>()
            //    .HasOne(c => c.City)
            //    .WithMany()
            //    .HasForeignKey(f => f.CityId);

            base.OnModelCreating(builder);

        }

    }
}
