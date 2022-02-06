using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTracking.Domain.Context;
using TimeTracking.Domain.Entities;

namespace TimeTracking.Web.Controllers
{
    public class WorkDaysController : BaseController
    {
        private readonly TimeTrackingDbContext _context;

        public WorkDaysController(TimeTrackingDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkDays.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDays = await _context.WorkDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDays == null)
            {
                return NotFound();
            }

            return View(workDays);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkDays workDays)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workDays);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workDays);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDays = await _context.WorkDays.FindAsync(id);
            if (workDays == null)
            {
                return NotFound();
            }
            return View(workDays);
        }

        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkDays workDays)
        {
            if (id != workDays.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workDays);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkDaysExists(workDays.Id))
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
            return View(workDays);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDays = await _context.WorkDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDays == null)
            {
                return NotFound();
            }

            return View(workDays);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workDays = await _context.WorkDays.FindAsync(id);
            _context.WorkDays.Remove(workDays);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkDaysExists(int id)
        {
            return _context.WorkDays.Any(e => e.Id == id);
        }
    }
}
