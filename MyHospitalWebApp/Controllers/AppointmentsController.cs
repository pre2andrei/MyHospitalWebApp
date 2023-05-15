using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalWebApp.Models;
using HospitalWebApp.Controllers.Helpers;
using MyHospitalWebApp.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using MyHospitalWebApp.Data.Excel;
using MyHospitalWebApp.Models.BindingModels;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace HospitalWebApp.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public AppointmentsController(AppDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
           // filterdList.filter = filterdList.filter ?? new AppointmentsFilter();

            List<Appointment> list;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Doctor"))
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.userId == userId);
                list = await _context.Appointments.Where(a => a.doctorId == doctor.Id).Include(a => a.Doctor).Include(a => a.Patient).ToListAsync();
            }
            else if (User.IsInRole("Patient"))
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.userId == userId);
                list = await _context.Appointments.Where(a => a.patientId == patient.Id).Include(a => a.Doctor).Include(a => a.Patient).ToListAsync();
            }
            else
            {
                list = await _context.Appointments.ToListAsync();
            }

            var data = new FilteredAppointmentsList()
            {
                filter = new AppointmentsFilter(),
                oldAppointments = list.Where(a => a.AppointedTime < DateTime.Now).ToList(),
                upcomingAppointments = list.Where(a => a.AppointedTime > DateTime.Now).ToList()
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppointmentsFilter filter)
        {
            filter = filter ?? new AppointmentsFilter();

            List<Appointment> list;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Doctor"))
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.userId == userId);
                list = await _context.Appointments.FilterAppointments(filter).Where(a => a.doctorId == doctor.Id).ToListAsync();
            }
            else if (User.IsInRole("Patient"))
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.userId == userId);
                list = await _context.Appointments.FilterAppointments(filter).Where(a => a.patientId == patient.Id).ToListAsync();
            }
            else
            {
                list = await _context.Appointments.FilterAppointments(filter).ToListAsync();
            }

            var data = new FilteredAppointmentsList()
            {
                filter = filter,
                oldAppointments = list.Where(a => a.AppointedTime < DateTime.Now).ToList(),
                upcomingAppointments = list.Where(a => a.AppointedTime > DateTime.Now).ToList()
            };

            return View(data);
        }


        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> CreateAsync()
        {
            var appointment = new Appointment();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["doctorId"] = _context.Doctors.toSelectList();
            ViewData["patientId"] = _context.Patients.toSelectList();
            if (User.IsInRole("Doctor"))
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.userId == userId);
                appointment.doctorId = doctor.Id;
            }
            else if (User.IsInRole("Patient"))
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.userId == userId);
                appointment.patientId = patient.Id;
            }

            return View(appointment);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,patientId,doctorId,AppointedTime,RoomNr")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["doctorId"] = _context.Doctors.toSelectList(appointment.doctorId);
            ViewData["patientId"] = _context.Patients.toSelectList(appointment.patientId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["doctorId"] = _context.Doctors.toSelectList(appointment.doctorId);
            ViewData["patientId"] = _context.Patients.toSelectList(appointment.patientId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,patientId,doctorId,AppointedTime,RoomNr")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
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
            ViewData["doctorId"] = _context.Doctors.toSelectList(appointment.doctorId);
            ViewData["patientId"] = _context.Patients.toSelectList(appointment.patientId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'AppDbContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> DownloadFile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Doctor"))
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.userId == userId);
                CloseXML.CreateAppointmentsExcel(
                    await _context.Appointments
                    .Where(a => a.doctorId == doctor.Id)
                    .Include(a => a.Doctor)
                    .Include(a => a.Patient)
                    .ToListAsync(), "wwwroot\\Appointments.xlsx");
            }
            else if (User.IsInRole("Patient"))
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.userId == userId);
                CloseXML.CreateAppointmentsExcel(
                    await _context.Appointments
                    .Where(a => a.patientId == patient.Id)
                    .Include(a => a.Doctor)
                    .Include(a => a.Patient)
                    .ToListAsync(), "wwwroot\\Appointments.xlsx");
            }
            else
            {
                CloseXML.CreateAppointmentsExcel(
                   await _context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .ToListAsync(), "wwwroot\\Appointments.xlsx");
            }

            var memory = CloseXML.DownloadList("Appointments.xlsx", "wwwroot");
            return File(memory.ToArray(), "application/vnd.ms-excel", "Appointments.xlsx");
        }

        public async Task<IActionResult> DownloadFileSP()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (User.IsInRole("Doctor"))
            {
                var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.userId == userId);
                MakeAppointmentsFile(doctor.Id, true);
            }
            else if (User.IsInRole("Patient"))
            {
                var patient = await _context.Patients.FirstOrDefaultAsync(d => d.userId == userId);
                MakeAppointmentsFile(patient.Id, false);
            }

            var memory = CloseXML.DownloadList("AppointmentsSP.xlsx", "wwwroot");
            return File(memory.ToArray(), "application/vnd.ms-excel", "AppointmentsSP.xlsx");
        }

        public void MakeAppointmentsFile(int Id, bool isDoctor)
        {
            var data = new List<object>();
            String SqlconString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand Cmnd =
                    isDoctor
                    ? new SqlCommand($"EXECUTE [dbo].[GetAppointmentsForDoctor] @ID", sqlCon)
                    : new SqlCommand($"EXECUTE [dbo].[GetAppointmentsForPatient] @ID", sqlCon);
                Cmnd.Parameters.AddWithValue("@ID", Id);
                SqlDataReader rdr = Cmnd.ExecuteReader();
                while (rdr.Read())
                {
                    data.Add(new
                    {
                        id = rdr["id"],
                        patientId = rdr["patientId"],
                        Patient = rdr["Patient"],
                        doctorId = rdr["doctorId"],
                        Doctor = rdr["Doctor"],
                        AppointedTime = rdr["AppointedTime"],
                        RoomNr = rdr["roomNr"]
                    });
                }
                sqlCon.Close();
            }
            CloseXML.CreateExcelWithSP(data, "wwwroot\\AppointmentsSP.xlsx");
        }
    }
}
