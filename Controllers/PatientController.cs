namespace ConsultasMedicas.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using ConsultasMedicas.Models;
    using ConsultasMedicas.Services;
    using ConsultasMedicas.ViewModels;
    using System.Threading.Tasks;

    public class PatientController : Controller
    {
        private readonly UserManager<Patient> _userManager;
        private readonly SignInManager<Patient> _signInManager;
        private readonly IPatientService _patientService;

        public PatientController(UserManager<Patient> userManager, SignInManager<Patient> signInManager, IPatientService patientService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _patientService = patientService;
        }

        // GET: Patient/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Patient/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient {UserName = model.UserName, Email = model.Email, FullName = model.FullName };
                var result = await _userManager.CreateAsync(patient, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(patient, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // GET: Patient/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Patient/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool rememberMe = model.RememberMe.Equals("true");
                var user = await _userManager.FindByNameAsync(model.UserName);
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, rememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        // GET: Patient/Profile
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email ?? user.NormalizedEmail.ToLower()
            };

            return View(model);
        }

        // POST: Patient/Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                user.FullName = model.FullName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // GET: Patient/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
