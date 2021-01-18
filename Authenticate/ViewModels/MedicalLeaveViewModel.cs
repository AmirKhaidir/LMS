using Authenticate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.ViewModels
{
    public class MedicalLeaveViewModel
    {
        [Display(Name="Medical Leave")]
        public List<MedicalLeaveModel> Medical { get; set; }

        public List<EntitlementModel> Entitlement { get; set; }
    }
}
