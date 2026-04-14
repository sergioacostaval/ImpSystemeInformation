using Microsoft.EntityFrameworkCore;
using StocksInvesthink.Data;
using StocksInvesthink.Models;
using StocksInvesthink.Controllers;

namespace NUTest
{
    public class Tests
    {

        [Test]
        public async Task Test_Creation_Utilisateur()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StocksInvesthinkContext>()
                .UseInMemoryDatabase("UserTestDb")
                .Options;

            using var context = new StocksInvesthinkContext(options);

            var user = new User("TestUser", "test@test.com", "hashedPassword");

            // Act
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userFromDb = await context.Users.FirstOrDefaultAsync(u => u.Email == "test@test.com");

            // Assert
            Assert.That(userFromDb, Is.Not.Null);
            Assert.That(userFromDb.Email, Is.EqualTo("test@test.com"));
        }

        [Test]
        public void Test_Hash_Password()
        {
            // Arrange
            string password = "Password123";

            // Act
            string hash1 = new AccountController(null).HashPassword(password);
            string hash2 = new AccountController(null).HashPassword(password);

            // Assert
            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public async Task Test_Authentification_UtilisateurExistant()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StocksInvesthinkContext>()
                .UseInMemoryDatabase("AuthTestDb")
                .Options;

            using var context = new StocksInvesthinkContext(options);

            string name = "TestUser";
            string email = "test@test.com";
            string password = "Password123";
            string passwordHash = new AccountController(null).HashPassword(password);

            var user = new User(name, email, passwordHash);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var userFromDb = await context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            bool isAuthenticated = userFromDb != null &&
                                   userFromDb.PasswordHash == new AccountController(null).HashPassword(password);
            // Assert
            Assert.That(isAuthenticated, "L'utilisateur doit être authentifie avec succes.");
        }
    }
}
