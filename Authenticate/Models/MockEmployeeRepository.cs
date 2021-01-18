using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _EmployeeList;

        public MockEmployeeRepository()
        {
            _EmployeeList = new List<Employee>()
            {
                new Employee(){Id = 1, Name = "Mary", Department= Dept.HR, Email = "Mary@gmail.com" },
                new Employee(){Id = 2, Name = "John", Department= Dept.IT, Email = "John@gmail.com" },
                new Employee(){Id = 3, Name = "Katheri", Department= Dept.Sales, Email = "Katheri@gmail.com" }
            };
        }
        public Employee Add(Employee Employee)
        {
           Employee.Id = _EmployeeList.Max(e => e.Id) + 1;
             _EmployeeList.Add(Employee);
            return Employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee = _EmployeeList.FirstOrDefault(e => e.Id == Id);
            if(employee != null)
            {
                _EmployeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _EmployeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return this._EmployeeList.FirstOrDefault(e => e.Id == Id);
        }

        public Employee Update(Employee EmployeeChanges)
        {
            Employee employee = _EmployeeList.FirstOrDefault(e => e.Id == EmployeeChanges.Id);
            if (employee != null)
            {
                employee.Name = EmployeeChanges.Name;
                employee.Email = EmployeeChanges.Email;
                employee.Department = EmployeeChanges.Department;
            }
            return employee;
        }
    }
}
