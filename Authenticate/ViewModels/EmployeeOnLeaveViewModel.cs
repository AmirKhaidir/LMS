using Authenticate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.ViewModels
{
    public class EmployeeOnLeaveViewModel
    {
        public List<ApplyLeaveModel> ApplyLeave { get; set; }

        public List<OtherLeaveModel> OtherLeave { get; set; }

        public List<MedicalLeaveModel> MedicalLeave { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
