using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Models
{
    public class ApplyLeaveModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LeaveType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime SubmittedDate { get; set; }


        public string Approve { get; set; }


        public string ApproveBy { get; set; }

        [Required]
        public string Session { get; set; }

        public string EmpId { get; set; }

        //public EntitlementModel entitlement { get; set; }
    }
}
