using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PPG.Models.Entites;

namespace PPG.Data
{
    public class ElectContext : DbContext
    {
        public ElectContext(DbContextOptions<ElectContext> options) : base(options) { }

        public DbSet<DecryptedVote> Votes { get; set; }
    }
}
