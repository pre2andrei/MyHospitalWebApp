using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Data;
using HospitalWebApp.Models;

namespace HospitalWebApp.Controllers
{
    public class SymptomsController : Controller
    {
        private readonly AppDbContext _context;

        public SymptomsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Symptoms
        public async Task<IActionResult> Index()
        {
              return _context.Symptoms != null ? 
                          View(await _context.Symptoms.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Symptoms'  is null.");
        }

        // GET: Symptoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Symptoms == null)
            {
                return NotFound();
            }

            var symptom = await _context.Symptoms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (symptom == null)
            {
                return NotFound();
            }

            return View(symptom);
        }

        // GET: Symptoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Symptoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Symptom symptom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(symptom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(symptom);
        }

        // GET: Symptoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Symptoms == null)
            {
                return NotFound();
            }

            var symptom = await _context.Symptoms.FindAsync(id);
            if (symptom == null)
            {
                return NotFound();
            }
            return View(symptom);
        }

        // POST: Symptoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Symptom symptom)
        {
            if (id != symptom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(symptom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SymptomExists(symptom.Id))
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
            return View(symptom);
        }

        // GET: Symptoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Symptoms == null)
            {
                return NotFound();
            }

            var symptom = await _context.Symptoms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (symptom == null)
            {
                return NotFound();
            }

            return View(symptom);
        }

        // POST: Symptoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Symptoms == null)
            {
                return Problem("Entity set 'AppDbContext.Symptoms'  is null.");
            }
            var symptom = await _context.Symptoms.FindAsync(id);
            if (symptom != null)
            {
                _context.Symptoms.Remove(symptom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SymptomExists(int id)
        {
          return (_context.Symptoms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
