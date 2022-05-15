using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTracking.Domain.Context;
using TimeTracking.Domain.Entities;

namespace TimeTracking.Web.Controllers
{
    public class ActionsLogsController : BaseController
    {
        private readonly TimeTrackingDbContext _context;

        public ActionsLogsController(TimeTrackingDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.ActionsLogs
                .Include(e => e.Project)
                .OrderBy(x => x.ActionDate).ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionsLog = await _context.ActionsLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actionsLog == null)
            {
                return NotFound();
            }

            return View(actionsLog);
        }


        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActionsLog actionsLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actionsLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actionsLog);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionsLog = await _context.ActionsLogs.FindAsync(id);
            if (actionsLog == null)
            {
                return NotFound();
            }
            return View(actionsLog);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActionsLog actionsLog)
        {
            if (id != actionsLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actionsLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActionsLogExists(actionsLog.Id))
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
            return View(actionsLog);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionsLog = await _context.ActionsLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actionsLog == null)
            {
                return NotFound();
            }

            return View(actionsLog);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actionsLog = await _context.ActionsLogs.FindAsync(id);
            _context.ActionsLogs.Remove(actionsLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActionsLogExists(int id)
        {
            return _context.ActionsLogs.Any(e => e.Id == id);
        }
    }
}
