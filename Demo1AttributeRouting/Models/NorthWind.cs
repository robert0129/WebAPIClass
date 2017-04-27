using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo1AttributeRouting.Models
{
    public class NorthWind : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public NorthWind(DbContextOptions<NorthWind> options)
            : base(options)
        {

        }
    }
}
