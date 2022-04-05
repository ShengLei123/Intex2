using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class UtahCrashDbContext : DbContext
    {
        public UtahCrashDbContext(DbContextOptions<UtahCrashDbContext> options) : base (options)
        {

        }
        public DbSet<Utah_Crash> Utah_Crash { get; set; }
    }
}
