using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedProgramming_Lesson3.Models
{
    public class OsobaContext : DbContext
    {
        public OsobaContext(DbContextOptions<OsobaContext> options) : base(options)
        {

        }

        public DbSet<OsobaItem> osobaItems { get; set; }
    }
}
