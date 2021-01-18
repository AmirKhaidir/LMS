using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authenticate.Data;
using Authenticate.Models;
using Authenticate.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authenticate.Controllers
{
    public class EmployeeOnLeaveController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplyLeaveContext _applyContext;
        private readonly MedicalLeaveContext _medicalContext;
        private readonly OtherLeaveContext _otherContext;

        public EmployeeOnLeaveController(UserManager<ApplicationUser> userManager, ApplyLeaveContext applyContext, MedicalLeaveContext medicalContext, OtherLeaveContext otherContext)
        {
            this.userManager = userManager;
            _applyContext = applyContext;
            _medicalContext = medicalContext;
            _otherContext = otherContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime date)
        {
            var apply3 = from a in _applyContext.ApplyLeave
                        select a;
            var medical3 = from m in _medicalContext.MedicalLeave
                          select m;
            var other3 = from o in _otherContext.OtherLeave
                        select o;

            var dateNow = DateTime.Now;

            var dateto = DateTime.Now;

            var d = DateTime.UtcNow;

            if(date != null)
            {
                if(date == DateTime.MinValue)
                {
                    var apply = from p in _applyContext.ApplyLeave
                                where (dateNow > p.StartDate) && (dateNow < p.EndDate)
                                select p;
                    var medical = from m in _medicalContext.MedicalLeave
                                  where (dateNow > m.StartDate) && (dateNow < m.EndDate)
                                  select m;
                    var other = from p in _otherContext.OtherLeave
                                where (dateNow > p.StartDate) && (dateNow < p.EndDate)
                                select p;

                    var model = new EmployeeOnLeaveViewModel
                    {
                        ApplyLeave = await apply.ToListAsync(),
                        MedicalLeave = await medical.ToListAsync(),
                        OtherLeave = await other.ToListAsync(),
                        Date = dateNow
                    };

                    return View(model);
                }

                var apply2 = from p in _applyContext.ApplyLeave
                         where (date > p.StartDate) && (date < p.EndDate)
                         select p;
                var medical2 = from m in _medicalContext.MedicalLeave
                           where (date > m.StartDate) && (date < m.EndDate)
                           select m;
                 var other2 = from p in _otherContext.OtherLeave
                         where (date > p.StartDate) && (date < p.EndDate)
                         select p;


                var model2 = new EmployeeOnLeaveViewModel
                {
                    ApplyLeave = await apply2.ToListAsync(),
                    MedicalLeave = await medical2.ToListAsync(),
                    OtherLeave = await other2.ToListAsync()
                };

                return View(model2);

            }

            return View();

            /*if(date != null)
            {
               

            */

            /*var model2 = new EmployeeOnLeaveModel();

            var dateNow = DateTime.Now;

            var apply2 = from p in _applyContext.ApplyLeave
                         where (dateNow > p.StartDate) && (dateNow < p.EndDate)
                         select p;
            var medical2 = from m in _medicalContext.MedicalLeave
                           where (dateNow > m.StartDate) && (dateNow < m.EndDate)
                           select m;
            var other2 = from p in _otherContext.OtherLeave
                         where (dateNow > p.StartDate) && (dateNow < p.EndDate)
                         select p;

            date = model2.Date;

            if(date != null)
            {

                 apply2 = from p in _applyContext.ApplyLeave
                          where (date > p.StartDate) && (date < p.EndDate)
                             select p;
                 medical2 = from m in _medicalContext.MedicalLeave
                               where (date > m.StartDate) && (date < m.EndDate)
                               select m;
                 other2 = from p in _otherContext.OtherLeave
                             where (date > p.StartDate) && (date < p.EndDate)
                             select p;
                model2.ApplyLeave = await apply2.ToListAsync();
                model2.MedicalLeave = await medical2.ToListAsync();
                model2.OtherLeave = await other2.ToListAsync();
                return View(model2);

            }

            model2.ApplyLeave = await apply2.ToListAsync();
            model2.MedicalLeave = await medical2.ToListAsync();
            model2.OtherLeave = await other2.ToListAsync();
            return View(model2);*/
        }
    }
}
