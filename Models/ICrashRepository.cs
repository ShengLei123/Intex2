using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Intex2.Models
{
    public interface ICrashRepository
    {
        IQueryable<Utah_Crash> Utah_Crash { get; }
        public void CreateAccident(Utah_Crash c);
        public void SaveAccident(Utah_Crash c);
        public void DeleteAccident(Utah_Crash c);
    }
}
