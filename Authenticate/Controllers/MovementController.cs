using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authenticate.Data;
using Authenticate.Models;

namespace Authenticate.Controllers
{
    public class MovementController : Controller
    {
        private readonly MovementContext _context;

        public MovementController(MovementContext context)
        {
            _context = context;
        }

        public IActionResult AdminEdit()
        {
            var model = from m in _context.Movement
                        where m.Status == "waiting"
                        orderby m.Status
                        select m;

            return View(model);
        }

        // GET: Movement/Details/5
        public async Task<IActionResult> AdminDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementModel = await _context.Movement
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (movementModel == null)
            {
                return NotFound();
            }

            return View(movementModel);
        }

        public async Task<IActionResult> AdminDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementModel = await _context.Movement
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (movementModel == null)
            {
                return NotFound();
            }

            return View(movementModel);
        }

        // POST: Movement/Delete/5
        [HttpPost, ActionName("AdminDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteConfirmed(int id)
        {
            var movementModel = await _context.Movement.FindAsync(id);
            _context.Movement.Remove(movementModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminEdit));
        }

        // GET: Movement
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movement.ToListAsync());
        }

        // GET: Movement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementModel = await _context.Movement
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (movementModel == null)
            {
                return NotFound();
            }

            return View(movementModel);
        }

        // GET: Movement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,Action,Code,TimeIn,TimeOut,Date,Location,DateSubmitted")] MovementModel movementModel)
        {
            if (ModelState.IsValid)
            {
                movementModel.Status = "waiting";
                _context.Add(movementModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movementModel);
        }

        // GET: Movement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementModel = await _context.Movement.FindAsync(id);
            if (movementModel == null)
            {
                return NotFound();
            }
            return View(movementModel);
        }

        // POST: Movement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,Action,Code,TimeIn,TimeOut,Date,Location,Status,DateSubmitted")] MovementModel movementModel)
        {
            if (id != movementModel.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movementModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovementModelExists(movementModel.EmployeeId))
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
            return View(movementModel);
        }

        // GET: Movement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movementModel = await _context.Movement
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (movementModel == null)
            {
                return NotFound();
            }

            return View(movementModel);
        }

        // POST: Movement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movementModel = await _context.Movement.FindAsync(id);
            _context.Movement.Remove(movementModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovementModelExists(int id)
        {
            return _context.Movement.Any(e => e.EmployeeId == id);
        }
    }
}
