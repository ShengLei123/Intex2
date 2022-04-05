using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public class EFCrashRepository : ICrashRepository
    {
        private UtahCrashDbContext _context { get; set; }
        public EFCrashRepository(UtahCrashDbContext temp)
        {
            _context = temp;
        }
        public IQueryable<Utah_Crash> Utah_Crash => _context.Utah_Crash;
    }
}
