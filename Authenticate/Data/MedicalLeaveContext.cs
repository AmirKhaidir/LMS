using Authenticate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Data
{
    public class MedicalLeaveContext : DbContext
    {
        public MedicalLeaveContext(DbContextOptions<MedicalLeaveContext> options)
            : base(options)
        {
        }

        public DbSet<MedicalLeaveModel> MedicalLeave { get; set; }

        public DbSet<EntitlementModel> Entitlement { get; set; }
    }
}
