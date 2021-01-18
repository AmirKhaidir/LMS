using Authenticate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Data
{
    public class ApplyOnBehalfContext : DbContext
    {
        public ApplyOnBehalfContext(DbContextOptions<ApplyOnBehalfContext> options)
            : base(options)
        {
        }

        public DbSet<ApplyOnBehalfModel> ApplyBehalf { get; set; }
    }
}
