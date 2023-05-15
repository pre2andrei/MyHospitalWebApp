using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Data;
using HospitalWebApp.Models;
using HospitalWebApp.Controllers.Helpers;

namespace HospitalWebApp.Controllers
{
    public class Doctor_SpecialityController : Controller
    {
        private readonly AppDbContext _context;

        public Doctor_SpecialityController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Doctor_Speciality
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Doctors_Specialities.Include(d => d.Doctor).Include(d => d.Speciality);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Doctor_Speciality/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doctors_Specialities == null)
            {
                return NotFound();
            }

            var doctor_Speciality = await _context.Doctors_Specialities
                .Include(d => d.Doctor)
                .Include(d => d.Speciality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor_Speciality == null)
            {
                return NotFound();
            }

            return View(doctor_Speciality);
        }

        // GET: Doctor_Speciality/Create
        public IActionResult Create(int docId)
        {
           // ViewData["doctorId"] = _context.Doctors.toSelectList(docId);
            ViewData["specialityId"] = _context.Specialities.toSelectList();
            return View(new Doctor_Speciality()
            {
                doctorId = docId,
            });
        }

        // POST: Doctor_Speciality/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,doctorId,specialityId")] Doctor_Speciality doctor_Speciality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor_Speciality);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("MyDetails","Doctors");
        }

        // GET: Doctor_Speciality/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doctors_Specialities == null)
            {
                return NotFound();
            }

            var doctor_Speciality = await _context.Doctors_Specialities.FindAsync(id);
            if (doctor_Speciality == null)
            {
                return NotFound();
            }
            ViewData["doctorId"] = _context.Doctors.toSelectList(doctor_Speciality.doctorId);
            ViewData["specialityId"] = _context.Specialities.toSelectList(doctor_Speciality.specialityId);
            return View(doctor_Speciality);
        }

        // POST: Doctor_Speciality/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,doctorId,specialityId")] Doctor_Speciality doctor_Speciality)
        {
            if (id != doctor_Speciality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor_Speciality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Doctor_SpecialityExists(doctor_Speciality.Id))
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
            ViewData["doctorId"] = _context.Doctors.toSelectList(doctor_Speciality.doctorId);
            ViewData["specialityId"] = _context.Specialities.toSelectList(doctor_Speciality.specialityId);
            return View(doctor_Speciality);
        }

        // GET: Doctor_Speciality/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doctors_Specialities == null)
            {
                return NotFound();
            }

            var doctor_Speciality = await _context.Doctors_Specialities
                .Include(d => d.Doctor)
                .Include(d => d.Speciality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor_Speciality == null)
            {
                return NotFound();
            }

            return View(doctor_Speciality);
        }

        // POST: Doctor_Speciality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doctors_Specialities == null)
            {
                return Problem("Entity set 'AppDbContext.Doctors_Specialities'  is null.");
            }
            var doctor_Speciality = await _context.Doctors_Specialities.FindAsync(id);
            if (doctor_Speciality != null)
            {
                _context.Doctors_Specialities.Remove(doctor_Speciality);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("MyDetails", "Doctors");
        }

        private bool Doctor_SpecialityExists(int id)
        {
          return (_context.Doctors_Specialities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
