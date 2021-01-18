using Authenticate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.ViewModels
{
    public class ApplyLeaveViewModel
    {
     
        public IEnumerable<ApplyLeaveModel> Apply { get; set; }

        public IEnumerable<EntitlementModel> Entitlement { get; set; }

    }
}
