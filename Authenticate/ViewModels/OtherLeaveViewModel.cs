using Authenticate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.ViewModels
{
    public class OtherLeaveViewModel
    {
        [Display(Name = "Other Leave")]
        public List<OtherLeaveModel> Other { get; set; }

        public List<EntitlementModel> Entitlement { get; set; }
    }
}
