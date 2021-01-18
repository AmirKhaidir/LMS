using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string city { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

    }
}
