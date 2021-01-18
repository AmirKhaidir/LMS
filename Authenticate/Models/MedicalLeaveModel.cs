using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Models
{
    public class MedicalLeaveModel
    {
        public int Id { get; set; }

        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }

        public string EmpId { get; set; }

        public int Year { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Submitted Date")]
        [DataType(DataType.Date)]
        public DateTime SubmittedDate { get; set; }

        [Required]
        public string Reason { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "File Path")]
        public string FilePath { get; set; }

        [NotMapped]
        [Display(Name = "File")]
        public IFormFile FileReason { get; set; }
    }
}
