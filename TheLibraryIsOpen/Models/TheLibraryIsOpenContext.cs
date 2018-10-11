using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Data
{
    public class TheLibraryIsOpenContext : DbContext
    {
        public TheLibraryIsOpenContext(DbContextOptions<TheLibraryIsOpenContext> options)
            : base(options)
        {
        }

        public DbSet<TheLibraryIsOpen.Models.DBModels.Book> Book { get; set; }
        public DbSet<TheLibraryIsOpen.Models.DBModels.Music> Music { get; set; }
        public DbSet<TheLibraryIsOpen.Models.DBModels.Magazine> Magazine { get; set; }
    }
}
