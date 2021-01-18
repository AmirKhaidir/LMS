using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Models
{
    public static class ModelBuildeExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Name = "Mark",
                   Department = Dept.HR,
                   Email = "Mark@gmail.com"
               },
               new Employee
               {
                   Id = 2,
                   Name = "Diegp",
                   Department = Dept.IT,
                   Email = "Diego@gmail.com"
               }

               );
        }
    }
}
