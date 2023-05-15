using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Data;
using HospitalWebApp.Models;
using HospitalWebApp.Controllers.Helpers;
using System.Security.Claims;

namespace HospitalWebApp.Controllers
{
    public class DiagnosticsController : Controller
    {
        private readonly AppDbContext _context;

        public DiagnosticsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Diagnostics
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Patient"))
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.userId == userId);
                var diagnostics = _context.Diagnostics.Include(d => d.Disease).Include(d => d.Patient).Where(d=>d.patientId == patient.Id);
                return View(await diagnostics.ToListAsync());
            }
            var appDbContext = _context.Diagnostics.Include(d => d.Disease).Include(d => d.Patient);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Diagnostics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diagnostics == null)
            {
                return NotFound();
            }

            var diagnostic = await _context.Diagnostics
                .Include(d => d.Disease)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostic == null)
            {
                return NotFound();
            }

            return View(diagnostic);
        }

        // GET: Diagnostics/Create
        public IActionResult Create(int patientId)
        {
            ViewData["diseaseId"] = _context.Diseases.toSelectList();
            return View(new Diagnostic()
            {
                patientId = patientId
            });
        }

        // POST: Diagnostics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,patientId,diseaseId")] Diagnostic diagnostic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diagnostic);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Patients",new { id = diagnostic.patientId});
        }

        // GET: Diagnostics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diagnostics == null)
            {
                return NotFound();
            }

            var diagnostic = await _context.Diagnostics.FindAsync(id);
            if (diagnostic == null)
            {
                return NotFound();
            }
            ViewData["diseaseId"] = _context.Diseases.toSelectList(diagnostic.diseaseId);
            ViewData["patientId"] = _context.Patients.toSelectList(diagnostic.patientId);
            return View(diagnostic);
        }

        // POST: Diagnostics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,patientId,diseaseId")] Diagnostic diagnostic)
        {
            if (id != diagnostic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnostic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticExists(diagnostic.Id))
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
            ViewData["diseaseId"] = _context.Diseases.toSelectList(diagnostic.diseaseId);
            ViewData["patientId"] = _context.Patients.toSelectList(diagnostic.patientId);
            return View(diagnostic);
        }

        // GET: Diagnostics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diagnostics == null)
            {
                return NotFound();
            }

            var diagnostic = await _context.Diagnostics
                .Include(d => d.Disease)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostic == null)
            {
                return NotFound();
            }

            return View(diagnostic);
        }

        // POST: Diagnostics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diagnostics == null)
            {
                return Problem("Entity set 'AppDbContext.Diagnostics'  is null.");
            }
            var diagnostic = await _context.Diagnostics.FindAsync(id);
            if (diagnostic != null)
            {
                _context.Diagnostics.Remove(diagnostic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Patients", new { id = diagnostic.patientId });
        }

        private bool DiagnosticExists(int id)
        {
          return (_context.Diagnostics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
