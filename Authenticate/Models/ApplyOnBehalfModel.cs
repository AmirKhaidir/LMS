using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Models
{
    public class ApplyOnBehalfModel
    {
        public int Id { get; set; }

        [Display(Name = "Employee Name")]
        public string EmpName { get; set; }

        [Display(Name = "Name On Behalf")]
        [Required]
        public string NameOnBehalf { get; set; }

        [Display(Name = "Date From")]
        [DataType(DataType.Date)]
        public DateTime DateApply { get; set; }

        [Display(Name = "Date To")]
        [DataType(DataType.Date)]
        public DateTime DateApply2 { get; set; }

        [Display(Name = "Submitted Date")]
        [DataType(DataType.Date)]
        public DateTime SubmittedDate { get; set; }

        [Required]
        public string Session { get; set; }

        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }

    }
}
