using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Data;
using MyHospitalWebApp.Models;

namespace MyHospitalWebApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Carss
        public async Task<IActionResult> Index()
        {
              return _context.Cars != null ? 
                          View(await _context.Cars.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Cars'  is null.");
        }

        // GET: Carss/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var Cars = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Cars == null)
            {
                return NotFound();
            }

            return View(Cars);
        }

        // GET: Carss/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Code")] Car Car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Car);
        }

        // GET: Carss/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var Cars = await _context.Cars.FindAsync(id);
            if (Cars == null)
            {
                return NotFound();
            }
            return View(Cars);
        }

        // POST: Carss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Code")] Car Car)
        {
            if (id != Car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarsExists(Car.Id))
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
            return View(Car);
        }

        // GET: Carss/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var Cars = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Cars == null)
            {
                return NotFound();
            }

            return View(Cars);
        }

        // POST: Carss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'AppDbContext.Cars'  is null.");
            }
            var Cars = await _context.Cars.FindAsync(id);
            if (Cars != null)
            {
                _context.Cars.Remove(Cars);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarsExists(int id)
        {
          return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
