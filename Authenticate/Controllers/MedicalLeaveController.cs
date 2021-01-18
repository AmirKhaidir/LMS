using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authenticate.Data;
using Authenticate.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Authenticate.ViewModels;

namespace Authenticate.Controllers
{
    public class MedicalLeaveController : Controller
    {
        private readonly MedicalLeaveContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment hostingEnvironment;

        public MedicalLeaveController(MedicalLeaveContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: MedicalLeave
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(HttpContext.User);

            var medicalLeave = from m in _context.MedicalLeave
                             where m.EmpId == userId
                             select m;

            var entitlement = from e in _context.Entitlement
                              where e.EmpId == userId && e.Type == "Medical"
                              select e;


            var model = new MedicalLeaveViewModel
            {
                Medical = await medicalLeave.ToListAsync(),
                Entitlement = await entitlement.ToListAsync()

            };

            return View(model);
        }

        // GET: MedicalLeave/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalLeaveModel = await _context.MedicalLeave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicalLeaveModel == null)
            {
                return NotFound();
            }

            return View(medicalLeaveModel);
        }

        // GET: MedicalLeave/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicalLeave/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LeaveType,StartDate,EndDate,Reason,FileName,FilePath,FileReason")] MedicalLeaveModel medicalLeaveModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if(medicalLeaveModel.FileReason != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "MedicalLeave");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + medicalLeaveModel.FileReason.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        medicalLeaveModel.FileReason.CopyTo(fileStream);
                    }
                }

                /*string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "OtherLeave");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileReason.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.FileReason.CopyTo(fileStream);
                }*/

                medicalLeaveModel.FileName = medicalLeaveModel.FileReason.FileName;
                medicalLeaveModel.FilePath = uniqueFileName;
                medicalLeaveModel.SubmittedDate = DateTime.Now;
                medicalLeaveModel.Year = DateTime.Now.Year;

                var userId = userManager.GetUserId(HttpContext.User);
                var user = await userManager.FindByIdAsync(userId);
                medicalLeaveModel.Name = user.FullName;
                medicalLeaveModel.EmpId = userId;

                TimeSpan dayTaken = medicalLeaveModel.EndDate - medicalLeaveModel.StartDate;
                var dayCount = dayTaken.TotalDays;
                var numDay = Convert.ToInt32(dayCount);

                var entitlement = from e in _context.Entitlement
                                  where e.EmpId == userId && e.Type == "Medical"
                                  select e;

                if(entitlement != null)
                {
                    var oldEntitlement = await entitlement.FirstOrDefaultAsync();


                    var taken = oldEntitlement.Taken + numDay;
                    var balance = oldEntitlement.Balance - numDay;
                    oldEntitlement.Taken = taken;
                    oldEntitlement.Balance = balance;

                    _context.Entitlement.Update(oldEntitlement);
                    await _context.SaveChangesAsync();
                }
               

                _context.Add(medicalLeaveModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicalLeaveModel);
        }

        // GET: MedicalLeave/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalLeaveModel = await _context.MedicalLeave.FindAsync(id);
            if (medicalLeaveModel == null)
            {
                return NotFound();
            }
            return View(medicalLeaveModel);
        }

        // POST: MedicalLeave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LeaveType,StartDate,EndDate,Reason,FileName,FilePath")] MedicalLeaveModel medicalLeaveModel)
        {
            if (id != medicalLeaveModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalLeaveModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalLeaveModelExists(medicalLeaveModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medicalLeaveModel);
        }

        // GET: MedicalLeave/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalLeaveModel = await _context.MedicalLeave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicalLeaveModel == null)
            {
                return NotFound();
            }

            return View(medicalLeaveModel);
        }

        // POST: MedicalLeave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalLeaveModel = await _context.MedicalLeave.FindAsync(id);
            _context.MedicalLeave.Remove(medicalLeaveModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalLeaveModelExists(int id)
        {
            return _context.MedicalLeave.Any(e => e.Id == id);
        }
    }
}
