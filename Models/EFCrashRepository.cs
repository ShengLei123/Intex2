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
        public void CreateAccident(Utah_Crash c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }
        public void SaveAccident(Utah_Crash c)
        {
            _context.Update(c);
            _context.SaveChanges();
        }
        public void DeleteAccident(Utah_Crash c)
        {
            _context.Remove(c);
            _context.SaveChanges();
        }
    }
}
