using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models
{
    public interface ICrashRepository
    {
        IQueryable<Utah_Crash> Utah_Crash { get; }

    }
}
