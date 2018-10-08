using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheLibraryIsOpen.Models.DBModels;

namespace TheLibraryIsOpen.Models
{
    public class TheLibraryIsOpenContext : DbContext
    {
        public TheLibraryIsOpenContext(DbContextOptions<TheLibraryIsOpenContext> options)
            : base(options)
        {
        }

        public DbSet<TheLibraryIsOpen.Models.DBModels.Magazine> Magazine { get; set; }
    }
}
