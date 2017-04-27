using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo1AttributeRouting.Models
{
    public class NorthWind : DbContext
    {
        public NorthWind(DbContextOptions<NorthWind> options)
            : base(options)
        {
        }
    }
}
