using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Models
{
    public class MovementModel
    {
        public MovementModel()
        {
        }

        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Code { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeIn { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeOut { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateSubmitted { get; set; }
    }
}
