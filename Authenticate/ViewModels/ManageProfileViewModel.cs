using Authenticate.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticate.ViewModels
{
    public class ManageProfileViewModel
    {

        public string Id { get; set; }

        public string FullName { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

    }
}
