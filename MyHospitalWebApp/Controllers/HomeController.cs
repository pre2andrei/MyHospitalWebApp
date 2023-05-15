using HospitalWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Data;
using MyHospitalWebApp.Data.Excel;
using MyHospitalWebApp.Models;
using MyHospitalWebApp.Models.BindingModels;
using System.Diagnostics;
using System.Linq;

namespace MyHospitalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private UserManager<IdentityUser> _userManager;
        private IPasswordHasher<IdentityUser> _passwordHasher;

        public HomeController(AppDbContext context, UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> passwordHasher)
        {
            _context = context;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
           // CloseXML.CreateAppointmentsExcel(_context.Appointments.Include(a=>a.Patient).Include(a=>a.Doctor).ToList(), "wwwroot\\Appointments.xlsx");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateIdentityPatient()//CreateIdentityPatientModel model
        {
            return View("CreateIdentityPatient");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateIdentityPatient([Bind("PIC,firstName,lastName,DOB,Email,password")] CreateIdentityPatientModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    NormalizedEmail = model.Email.ToUpper(),
                    NormalizedUserName = model.Email.ToUpper(),
                    EmailConfirmed = true
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.password);
                if (!result.Succeeded)
                    return View(model);

                var savedUser = await _userManager.FindByEmailAsync(model.Email);

                var patient = new Patient()
                {
                    firstName = model.firstName,
                    lastName = model.lastName,
                    DOB = model.DOB,
                    PIC = model.PIC,
                    userId = savedUser.Id,
                };
                await _userManager.AddToRoleAsync(savedUser, "Patient");
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult CreateIdentityDoctor()//CreateIdentityPatientModel model
        {
            return View("CreateIdentityDoctor");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateIdentityDoctor([Bind("PIC,firstName,lastName,Email,password")] CreateIdentityDoctorModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    NormalizedEmail = model.Email.ToUpper(),
                    NormalizedUserName = model.Email.ToUpper(),
                    EmailConfirmed = true
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.password);
                if (!result.Succeeded)
                    return View(model);

                var savedUser = await _userManager.FindByEmailAsync(model.Email);

                var doctor = new Doctor()
                {
                    firstName = model.firstName,
                    lastName = model.lastName,
                    PIC = model.PIC,
                    userId = savedUser.Id,
                };
                await _userManager.AddToRoleAsync(savedUser, "Doctor");
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}