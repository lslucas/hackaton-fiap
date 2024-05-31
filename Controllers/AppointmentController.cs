using ConsultasMedicas.Data;
using ConsultasMedicas.Helpers;
using ConsultasMedicas.Models;
using ConsultasMedicas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsultasMedicas.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var appointments = await _context.Appointments
                .Where(e => e.PatientId == userId)
                .Where(e => e.IsCancelled == false)
                .ToListAsync();

            var doctors = _context.Doctors;

            var viewModel = new AppointmentViewModel
            {
                Appointments = appointments,
                Doctor = doctors
            };
            return View(viewModel);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            var doctors = _context.Doctors;

            ViewData["DoctorId"] = new SelectList(doctors, "Id", "FullName");
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,DoctorId,Date")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.FirstOrDefault(e => e.Id == userId);

                EmailHelper.SendEmail(user.NormalizedEmail, "Novo agendamento de consulta", $"Nova consulta agendada para a data de {appointment.Date}");

                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "FullName", appointment.DoctorId);
            return View(appointment);
        }

        [HttpGet]
        public async Task<IActionResult> Delete([Bind("Id")] Appointment appointment)
        {
            var appoint = await _context.Appointments.FirstOrDefaultAsync(e => e.Id == appointment.Id);

            if (appoint == null)
            {
                return NotFound();
            }

            appoint.IsCancelled = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
