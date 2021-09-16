using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedProgramming_Lesson3.Models
{
    public class KsiazkaContext : DbContext
    {
        public KsiazkaContext(DbContextOptions<KsiazkaContext> options) : base(options)
        {

        }

        public DbSet<KsiazkaItem> ksiazkaItems { get; set; }
    }
}
