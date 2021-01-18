﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.ViewModels
{
    public class ManageEntitlementViewModel
    {


        public string EmpId { get; set; }

        public int Entitlement { get; set; }

        public int Balance { get; set; }

        public int Taken { get; set; }

        public string Type { get; set; }
    }
}
