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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Authenticate.ViewModels;

namespace Authenticate.Controllers
{
    public class OtherLeaveController : Controller
    {
        private readonly OtherLeaveContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public OtherLeaveController(OtherLeaveContext context, IHostingEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        // GET: OtherLeaveModels
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(HttpContext.User);

            var applyLeave = from m in _context.OtherLeave
                             where m.EmpId == userId
                             select m;

            var entitlement = from e in _context.Entitlement
                              where e.EmpId == userId && e.Type == "Other"
                              select e;


            var model = new OtherLeaveViewModel
            {
                Other = await applyLeave.ToListAsync(),
                Entitlement = await entitlement.ToListAsync()

            };

            return View(model);
        }

        // GET: OtherLeaveModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherLeaveModel = await _context.OtherLeave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherLeaveModel == null)
            {
                return NotFound();
            }

            return View(otherLeaveModel);
        }

        // GET: OtherLeaveModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OtherLeaveModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LeaveType,StartDate,EndDate,Reason,FileName,FilePath,FileReason")] OtherLeaveModel otherLeaveModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(otherLeaveModel);

                otherLeaveModel.FilePath = uniqueFileName;
                otherLeaveModel.FileName = otherLeaveModel.FileReason.FileName;

                var userId = userManager.GetUserId(HttpContext.User);
                var user = await userManager.FindByIdAsync(userId);
                otherLeaveModel.Name = user.FullName;
                otherLeaveModel.EmpId = userId;
                otherLeaveModel.SubmittedDate = DateTime.Now;
                _context.Add(otherLeaveModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(otherLeaveModel);
        }

        // GET: OtherLeaveModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherLeaveModel = await _context.OtherLeave.FindAsync(id);
            if (otherLeaveModel == null)
            {
                return NotFound();
            }
            return View(otherLeaveModel);
        }

        // POST: OtherLeaveModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LeaveType,StartDate,EndDate,Reason,FileName,FilePath")] OtherLeaveModel otherLeaveModel)
        {
            if (id != otherLeaveModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(otherLeaveModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OtherLeaveModelExists(otherLeaveModel.Id))
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
            return View(otherLeaveModel);
        }

        // GET: OtherLeaveModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var otherLeaveModel = await _context.OtherLeave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (otherLeaveModel == null)
            {
                return NotFound();
            }

            return View(otherLeaveModel);
        }

        // POST: OtherLeaveModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var otherLeaveModel = await _context.OtherLeave.FindAsync(id);
            _context.OtherLeave.Remove(otherLeaveModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OtherLeaveModelExists(int id)
        {
            return _context.OtherLeave.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile(OtherLeaveModel model)
        {
            string uniqueFileName = null;

            if (model.FileReason != null)
            {

                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "OtherLeave");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileReason.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.FileReason.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
    }
}
