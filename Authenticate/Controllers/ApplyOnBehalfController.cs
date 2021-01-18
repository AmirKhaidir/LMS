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

namespace Authenticate.Controllers
{
    public class ApplyOnBehalfController : Controller
    {
        private readonly ApplyOnBehalfContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplyOnBehalfController(ApplyOnBehalfContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: ApplyOnBehalf
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplyBehalf.ToListAsync());
        }

        // GET: ApplyOnBehalf/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyOnBehalfModel = await _context.ApplyBehalf
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applyOnBehalfModel == null)
            {
                return NotFound();
            }

            return View(applyOnBehalfModel);
        }

        // GET: ApplyOnBehalf/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplyOnBehalf/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpName,DateApply,DateApply2,Session,LeaveType,NameOnBehalf")] ApplyOnBehalfModel applyOnBehalfModel)
        {
            if (ModelState.IsValid)
            {
                applyOnBehalfModel.SubmittedDate = DateTime.Now;
                _context.Add(applyOnBehalfModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applyOnBehalfModel);
        }

        // GET: ApplyOnBehalf/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyOnBehalfModel = await _context.ApplyBehalf.FindAsync(id);
            if (applyOnBehalfModel == null)
            {
                return NotFound();
            }
            return View(applyOnBehalfModel);
        }

        // POST: ApplyOnBehalf/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpName,DateApply,DateApply2,Session,LeaveType,NameOnBehalf")] ApplyOnBehalfModel applyOnBehalfModel)
        {
            if (id != applyOnBehalfModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applyOnBehalfModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplyOnBehalfModelExists(applyOnBehalfModel.Id))
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
            return View(applyOnBehalfModel);
        }

        // GET: ApplyOnBehalf/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applyOnBehalfModel = await _context.ApplyBehalf
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applyOnBehalfModel == null)
            {
                return NotFound();
            }

            return View(applyOnBehalfModel);
        }

        // POST: ApplyOnBehalf/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applyOnBehalfModel = await _context.ApplyBehalf.FindAsync(id);
            _context.ApplyBehalf.Remove(applyOnBehalfModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplyOnBehalfModelExists(int id)
        {
            return _context.ApplyBehalf.Any(e => e.Id == id);
        }
    }
}
