using Authenticate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Models
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SqlEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Employee Add(Employee Employee)
        {
            context.Employee.Add(Employee);
            context.SaveChanges();
            return Employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = context.Employee.Find(Id);
            if(employee != null)
            {
                context.Employee.Remove(employee);
                context.SaveChanges();
            }

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employee;
        }

        public Employee GetEmployee(int Id)
        {
            return context.Employee.Find(Id);
        }

        public Employee Update(Employee EmployeeChanges)
        {
            var employee = context.Employee.Attach(EmployeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return EmployeeChanges;
        }
    }
}
