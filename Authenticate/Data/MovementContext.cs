using Authenticate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Data
{
    public class MovementContext : DbContext
    {
        public MovementContext(DbContextOptions<MovementContext> options)
            : base(options)
        {
        }

        public DbSet<MovementModel> Movement { get; set; }
    }
}
