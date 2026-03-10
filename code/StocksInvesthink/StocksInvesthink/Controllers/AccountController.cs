using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using StocksInvesthink.Data;
using StocksInvesthink.Models;

namespace StocksInvesthink.Controllers
{
    public class AccountController : Controller
    {
        private readonly StocksInvesthinkContext _context;

        // Injection du DbContext pour accéder à la base de données
        public AccountController(StocksInvesthinkContext context)
        {
            _context = context;
        }

        // -----------------------------
        // REGISTER
        // -----------------------------

        // Affiche la page d'inscription
        public IActionResult Register()
        {
            return View();
        }

        // Traitement du formulaire d'inscription
        [HttpPost]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            // Vérifier si l'utilisateur existe déjà
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (existingUser != null)
            {
                ViewBag.Error = "Cet email est déjà utilisé.";
                return View();
            }

            // Validation des règles du mot de passe selon le SRS
            if (!IsValidPassword(password))
            {
                ViewBag.Error = "Le mot de passe doit contenir au minimum 10 caractères, une majuscule, une minuscule et un chiffre.";
                return View();
            }

            // Hachage du mot de passe avant stockage en base
            string passwordHash = HashPassword(password);

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        // -----------------------------
        // LOGIN
        // -----------------------------

        // Affiche la page de connexion
        public IActionResult Login()
        {
            return View();
        }

        // Traitement du formulaire de connexion
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            // Vérification de l'utilisateur et du mot de passe
            if (user == null || user.PasswordHash != HashPassword(password))
            {
                ViewBag.Error = "Email ou mot de passe incorrect.";
                return View();
            }

            // Création des claims pour l'utilisateur connecté
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // Permet de garder la session active
                IsPersistent = true
            };

            // Création du cookie d'authentification
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        // -----------------------------
        // LOGOUT
        // -----------------------------

        // Déconnexion de l'utilisateur
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        // -----------------------------
        // MÉTHODES UTILITAIRES
        // -----------------------------

        // Fonction pour hacher un mot de passe avec SHA256
        private string HashPassword(string password)
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