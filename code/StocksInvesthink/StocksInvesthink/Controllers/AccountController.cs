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
            // Vérifier si l'email existe déjà
            bool emailExists = await _context.Users.AnyAsync(u => u.Email == email);
            if (emailExists)
            {
                ViewBag.Error = "Cet email est déjà utilisé.";
                return View();
            }

            // Vérifier le mot de passe selon les règles demandées
            if (!IsValidPassword(password))
            {
                ViewBag.Error = "Le mot de passe doit contenir au moins 10 caractères, une majuscule, une minuscule et un chiffre.";
                return View();
            }

            // Hacher le mot de passe avant sauvegarde
            string passwordHash = HashPassword(password);

            // Créer un nouvel utilisateur avec encapsulation
            var user = new User(name, email, passwordHash);

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