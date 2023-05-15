using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Data;
using HospitalWebApp.Models;

namespace HospitalWebApp.Controllers
{
    public class SpecialitiesController : Controller
    {
        private readonly AppDbContext _context;

        public SpecialitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Specialities
        public async Task<IActionResult> Index()
        {
              return _context.Specialities != null ? 
                          View(await _context.Specialities.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Specialities'  is null.");
        }

        // GET: Specialities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Specialities == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // GET: Specialities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speciality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speciality);
        }

        // GET: Specialities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Specialities == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities.FindAsync(id);
            if (speciality == null)
            {
                return NotFound();
            }
            return View(speciality);
        }

        // POST: Specialities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Speciality speciality)
        {
            if (id != speciality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speciality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialityExists(speciality.Id))
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
            return View(speciality);
        }

        // GET: Specialities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Specialities == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Specialities == null)
            {
                return Problem("Entity set 'AppDbContext.Specialities'  is null.");
            }
            var speciality = await _context.Specialities.FindAsync(id);
            if (speciality != null)
            {
                _context.Specialities.Remove(speciality);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialityExists(int id)
        {
          return (_context.Specialities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
