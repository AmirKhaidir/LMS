using Authenticate.Models;
using Authenticate.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Data
{
    public class ApplyLeaveContext : DbContext
    {
        public ApplyLeaveContext()
        {

        }

        public ApplyLeaveContext(DbContextOptions<ApplyLeaveContext> options)
            : base(options)
        {
        }

        public DbSet<ApplyLeaveModel> ApplyLeave{ get; set; }
        public DbSet<ApplyLeaveViewModel> ApplyLeaveView { get; set; }

        public DbSet<EntitlementModel> Entitlement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplyLeaveViewModel>().HasNoKey();
        }
    }
}
