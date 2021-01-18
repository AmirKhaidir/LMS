using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Models
{
    public class OtherLeaveModel
    {
        public int Id { get; set; }

        public string EmpId { get; set; }

        public string Name { get; set; }

        public string LeaveType { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime SubmittedDate { get; set; }

        public string Reason { get; set; }

        [NotMapped]
        public IFormFile FileReason { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

    }
}
