using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Data;
using HospitalWebApp.Models;
using HospitalWebApp.Controllers.Helpers;

namespace HospitalWebApp.Controllers
{
    public class Symptom_DiseaseController : Controller
    {
        private readonly AppDbContext _context;

        public Symptom_DiseaseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Symptom_Disease
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Symptoms_Diseases.Include(s => s.Disease).Include(s => s.Symptom);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Symptom_Disease/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Symptoms_Diseases == null)
            {
                return NotFound();
            }

            var symptom_Disease = await _context.Symptoms_Diseases
                .Include(s => s.Disease)
                .Include(s => s.Symptom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (symptom_Disease == null)
            {
                return NotFound();
            }

            return View(symptom_Disease);
        }

        // GET: Symptom_Disease/Create
        public IActionResult Create()
        {
            ViewData["DiseaseId"] = _context.Diseases.toSelectList();
            ViewData["SymptomId"] = _context.Symptoms.toSelectList();
            return View();
        }

        // POST: Symptom_Disease/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiseaseId,SymptomId")] Symptom_Disease symptom_Disease)
        {
            if (ModelState.IsValid)
            {
                _context.Add(symptom_Disease);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiseaseId"] = _context.Diseases.toSelectList(symptom_Disease.DiseaseId);
            ViewData["SymptomId"] = _context.Symptoms.toSelectList(symptom_Disease.SymptomId);
            return View(symptom_Disease);
        }

        // GET: Symptom_Disease/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Symptoms_Diseases == null)
            {
                return NotFound();
            }

            var symptom_Disease = await _context.Symptoms_Diseases.FindAsync(id);
            if (symptom_Disease == null)
            {
                return NotFound();
            }
            ViewData["DiseaseId"] = _context.Diseases.toSelectList(symptom_Disease.DiseaseId);
            ViewData["SymptomId"] = _context.Symptoms.toSelectList(symptom_Disease.SymptomId);
            return View(symptom_Disease);
        }

        // POST: Symptom_Disease/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DiseaseId,SymptomId")] Symptom_Disease symptom_Disease)
        {
            if (id != symptom_Disease.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(symptom_Disease);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Symptom_DiseaseExists(symptom_Disease.Id))
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
            ViewData["DiseaseId"] = _context.Diseases.toSelectList(symptom_Disease.DiseaseId);
            ViewData["SymptomId"] = _context.Symptoms.toSelectList(symptom_Disease.SymptomId);
            return View(symptom_Disease);
        }

        // GET: Symptom_Disease/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Symptoms_Diseases == null)
            {
                return NotFound();
            }

            var symptom_Disease = await _context.Symptoms_Diseases
                .Include(s => s.Disease)
                .Include(s => s.Symptom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (symptom_Disease == null)
            {
                return NotFound();
            }

            return View(symptom_Disease);
        }

        // POST: Symptom_Disease/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Symptoms_Diseases == null)
            {
                return Problem("Entity set 'AppDbContext.Symptoms_Diseases'  is null.");
            }
            var symptom_Disease = await _context.Symptoms_Diseases.FindAsync(id);
            if (symptom_Disease != null)
            {
                _context.Symptoms_Diseases.Remove(symptom_Disease);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Symptom_DiseaseExists(int id)
        {
          return (_context.Symptoms_Diseases?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
