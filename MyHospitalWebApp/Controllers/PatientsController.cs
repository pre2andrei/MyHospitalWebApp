using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Data;
using HospitalWebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MyHospitalWebApp.Data.Excel;
using HospitalWebApp.Controllers.Helpers;
using Microsoft.Data.SqlClient;
using MyHospitalWebApp.Models.BindingModels;

namespace HospitalWebApp.Controllers
{
    public class PatientsController : Controller
    {
        private readonly AppDbContext _context;
        private IConfiguration _configuration;

        public PatientsController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {

            var filteredPatients = new FilteredPatientsList()
            {
                filter = new PatientsFilter(),
                patients = await _context.Patients.ToListAsync()
            };

            return View(filteredPatients);
        }
        [HttpPost]
        public async Task<IActionResult> Index(PatientsFilter filter)
        {
            var filteredPatients = new FilteredPatientsList()
            {
                filter = filter,
                patients = await _context.Patients.FilterPatients(filter).ToListAsync()
            };

            return View(filteredPatients);
        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> MyDetails()
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _context.Patients
                .Include(p => p.Diagnostics).ThenInclude(diag => diag.Disease)
                .Include(p => p.Appointments).ThenInclude(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.userId == userId);
            if (patient == null)
            {
                return NotFound();
            }

            return View("Details", patient);
        }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Details(int id)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients
                .Include(p => p.Diagnostics).ThenInclude(diag => diag.Disease)
                .Include(p => p.Appointments).ThenInclude(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PIC,firstName,lastName,DOB")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PIC,firstName,lastName,DOB,userId")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'AppDbContext.Patients'  is null.");
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return (_context.Patients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public IActionResult DownloadFile()
        {
            CloseXML.CreatePatientsExcel(_context.Patients.ToList(), "wwwroot\\Patients.xlsx");
            var memory = CloseXML.DownloadList("Patients.xlsx", "wwwroot");
            return File(memory.ToArray(), "application/vnd.ms-excel", "Patients.xlsx");
        }

        public IActionResult DownloadFileSP()
        {
            MakePatientsFile();
            var memory = CloseXML.DownloadList("PatientsSP.xlsx", "wwwroot");
            return File(memory.ToArray(), "application/vnd.ms-excel", "PatientsSP.xlsx");
        }

        private void MakePatientsFile()
        {
            var data = new List<object>();
            String SqlconString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand Cmnd = new SqlCommand($"EXECUTE [dbo].[GetPatients]", sqlCon);
                SqlDataReader rdr = Cmnd.ExecuteReader();
                while (rdr.Read())
                {
                    data.Add(new
                    {
                        id = rdr["id"],
                        firstName = rdr["first_name"],
                        lastName = rdr["last_name"],
                        PIC = rdr["PIC"],
                        DOB = rdr["DOB"],
                        userId = rdr["user_id"],
                    });
                }
                sqlCon.Close();
            }
            CloseXML.CreateExcelWithSP(data, "wwwroot\\PatientsSP.xlsx");
        }
    }
}
