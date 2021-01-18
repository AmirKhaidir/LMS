using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authenticate.Data;
using Authenticate.Models;
using Microsoft.AspNetCore.Identity;
using ReflectionIT.Mvc.Paging;
using Authenticate.ViewModels;

namespace Authenticate.Controllers
{
    public class ApplyLeaveController : Controller
    {
        private readonly ApplyLeaveContext _context;
        private readonly MedicalLeaveContext medicalContext;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplyLeaveController(ApplyLeaveContext context, UserManager<ApplicationUser> userManager, MedicalLeaveContext medicalLeaveContext)
        {
            _context = context;
            medicalContext = medicalLeaveContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(HttpContext.User);

            var applyLeave = from m in _context.ApplyLeave
                             where m.EmpId == userId
                             select m;

            var entitlement = from e in _context.Entitlement
                              where e.EmpId == userId && (e.Type == "Annual" || e.Type == "Emergency")
                              select e;


            var model = new ApplyLeaveViewModel
            {
                Apply = await applyLeave.ToListAsync(),
                Entitlement = await entitlement.ToListAsync()
            
            };


            return View(model);

            /*var query = _context.ApplyLeave.AsNoTracking().OrderBy(s => s.Approve);

           var model = await PagingList.CreateAsync(query, 5, page);
           ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           ViewData["StatusSortParm"] = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
           var li = from s in _context.ApplyLeave
                    select s;
           switch (sortOrder)
           {
               case "name_desc":
                   li = li.OrderByDescending(s => s.Name);
                   break;
               case "status_desc":
                   li = li.OrderByDescending(s => s.Approve);
                   break;
           }
           await li.AsNoTracking().ToListAsync();*/
        }



        // GET: ApplyLeave/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyLeaveModel = await _context.ApplyLeave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applyLeaveModel == null)
            {
                return NotFound();
            }

            return View(applyLeaveModel);
        }

        // GET: ApplyLeave/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplyLeave/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LeaveType,StartDate,EndDate,Session")] ApplyLeaveModel applyLeaveModel)
        {
            if (ModelState.IsValid)
            {
                var userId = userManager.GetUserId(HttpContext.User);
                var user = await userManager.FindByIdAsync(userId);
                applyLeaveModel.Name = user.FullName;
                applyLeaveModel.EmpId = userId;
                applyLeaveModel.Approve = "Waiting";
                var dateNow = DateTime.Now;
                applyLeaveModel.SubmittedDate = dateNow;

                TimeSpan difference = applyLeaveModel.StartDate - dateNow;
                var days = difference.TotalDays;

                TimeSpan dayTaken = applyLeaveModel.EndDate - applyLeaveModel.StartDate;
                var dayCount = dayTaken.TotalDays;
                var numDay = Convert.ToInt32(dayCount);

                if (days <= 3)
                {
                    applyLeaveModel.LeaveType = "Emergency Leave";
                    var entitlement = from e in _context.Entitlement
                                      where e.EmpId == userId &&  e.Type == "Emergency"
                                      select e;
                    var oldEntitlement = await entitlement
                .FirstOrDefaultAsync();

                    var taken = oldEntitlement.Taken + numDay;
                    var balance = oldEntitlement.Balance - numDay;
                    oldEntitlement.Taken = taken;
                    oldEntitlement.Balance = balance;

                    if (oldEntitlement.Balance == 0)
                    {
                        ViewBag.ErrorMessage = "You does not have enough balanced to apply leave";
                        return View("NotFound");
                    }

                    _context.Entitlement.Update(oldEntitlement);
                    await _context.SaveChangesAsync();

                }

                else
                {
                    applyLeaveModel.LeaveType = "Annual Leave";
                    var entitlement = from e in _context.Entitlement
                                      where e.EmpId == userId && e.Type == "Annual"
                                      select e;
                    var oldEntitlement = await entitlement
                .FirstOrDefaultAsync();

                    var taken = oldEntitlement.Taken + numDay;
                    var balance = oldEntitlement.Balance - numDay;
                    oldEntitlement.Taken = taken;
                    oldEntitlement.Balance = balance;

                    if(oldEntitlement.Balance <= 0)
                    {
                        ViewBag.ErrorMessage = "You does not have enough balanced to apply leave";
                        return View("NotFound");
                    }
                    _context.Entitlement.Update(oldEntitlement);
                    await _context.SaveChangesAsync();

                }


                _context.Add(applyLeaveModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applyLeaveModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? empId)
        {
            if (empId == null)
            {
                return NotFound();
            }

            var applyLeaveModel = from a in _context.ApplyLeave
                                  where a.EmpId == empId
                                  select a;

            if (applyLeaveModel == null)
            {
                return NotFound();
            }

            List<ApplyLeaveModel> apply = await applyLeaveModel.Distinct().ToListAsync();
            
            return View(await applyLeaveModel.FirstOrDefaultAsync());
        }

        // POST: ApplyLeave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string empId, [Bind("Id,EmpId,Name,LeaveType,StartDate,EndDate,Approve,ApproveBy,Session")] ApplyLeaveModel applyLeaveModel)
        {

            if (empId != applyLeaveModel.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applyLeaveModel.EmpId = empId;
                    _context.Update(applyLeaveModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplyLeaveModelExists(applyLeaveModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminEdit));
            }
            return View(applyLeaveModel);
        }

        // GET: ApplyLeave/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyLeaveModel = await _context.ApplyLeave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applyLeaveModel == null)
            {
                return NotFound();
            }

            return View(applyLeaveModel);
        }

        // POST: ApplyLeave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applyLeaveModel = await _context.ApplyLeave.FindAsync(id);
            _context.ApplyLeave.Remove(applyLeaveModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplyLeaveModelExists(int id)
        {
            return _context.ApplyLeave.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> AdminEdit()
        {

            var applyLeave = from m in _context.ApplyLeave
                             where m.Approve == "waiting"
                             orderby m.LeaveType 
                             select m;


            var model = new ApplyLeaveViewModel
            {
                Apply = await applyLeave.ToListAsync(),
                

            };


            return View(model);
        }

        public async Task<IActionResult> AdminDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyLeaveModel = await _context.ApplyLeave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applyLeaveModel == null)
            {
                return NotFound();
            }

            return View(applyLeaveModel);
        }

        public async Task<IActionResult> AdminDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyLeaveModel = await _context.ApplyLeave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applyLeaveModel == null)
            {
                return NotFound();
            }

            return View(applyLeaveModel);
        }

        // POST: ApplyLeave/Delete/5
        [HttpPost, ActionName("AdminDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteConfirmed(int id)
        {
            var applyLeaveModel = await _context.ApplyLeave.FindAsync(id);
            _context.ApplyLeave.Remove(applyLeaveModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminDetails));
        }



    }
}
