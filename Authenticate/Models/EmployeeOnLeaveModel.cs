using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Authenticate.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Authenticate.Models
{
    public class EmployeeOnLeaveModel
    {
        public List<ApplyLeaveModel> ApplyLeave {get;set;}

        public List<OtherLeaveModel> OtherLeave { get; set; }

        public List<MedicalLeaveModel> MedicalLeave { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        //public ApplyLeaveModel ApplyLeave { get; set; }

        //public OtherLeaveModel OtherLeave { get; set; }

        //public MedicalLeaveModel MedicalLeave { get; set; }
    }
}
