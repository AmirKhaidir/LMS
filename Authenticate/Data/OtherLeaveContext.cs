using Authenticate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Data
{
    public class OtherLeaveContext : DbContext
    {
        public OtherLeaveContext(DbContextOptions<OtherLeaveContext> options)
            : base(options)
        {
        }

        public DbSet<OtherLeaveModel> OtherLeave { get; set; }

        public DbSet<EntitlementModel> Entitlement { get; set; }
    }
}
