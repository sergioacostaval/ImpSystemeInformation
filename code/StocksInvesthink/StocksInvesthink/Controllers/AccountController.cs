using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace StocksInvesthink.Controllers
{
    public class AccountController : Controller
    {
        private readonly StocksInvesthinkContext _context;

        public AccountController(StocksInvesthinkContext context)
        {
            _context = context;
        }

        // Affiche la page d'inscription
        public IActionResult Register()
        {
            return View();
        }

        // Traitement du formulaire d'inscription
        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == email);
            if (emailExists)
            {
                ViewBag.Error = "Cet email est déjà utilisé.";
                return View();
            }

            if (!IsValidPassword(password))
            {
                ViewBag.Error = "Le mot de passe doit contenir au moins 10 caractères, une majuscule, une minuscule et un chiffre.";
                return View();
            }

            string passwordHash = HashPassword(password);
            var user = new User(name, email, passwordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        // LOGIN
        public IActionResult Login()
        {
            return View();
        }

        // Formulaire de connexion
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.PasswordHash != HashPassword(password))
            {
                ViewBag.Error = "Email ou mot de passe incorrect.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        // LOGOUT
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        // MÉTHODES NECESSAIRES

        // Fonction pour hacher un mot de passe avec SHA256
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

        // Vérifie que le mot de passe respecte les règles définies dans le SRS
        private bool IsValidPassword(string password)
        {
            if (password.Length < 10)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);

            return hasUpper && hasLower && hasDigit;
        }
    }
}