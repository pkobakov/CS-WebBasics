namespace BattleCards.Services
{
    using BattleCards.Data;
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using SUS.MvcFramework;

    public class UserService : IUserService
    {
        private ApplicationDbContext data;

        public UserService()
        {
            this.data = new ApplicationDbContext();
        }
        public string CreateUser(string username, string email, string password)
        {
            var user = new User
            {

                Username = username,
                Email = email,
                Role = IdentityRole.User,
                Password = HashPassword(password)
            
            };

            this.data.Users.Add(user);
            this.data.SaveChanges();

            return user.Id;
        }

        public bool IsEmailAvailable(string email)
        {
           return !this.data.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.data.Users.Any(u=>u.Username == username);
        }

        public string GetUserId(string username, string password)
        {
            var user = this.data.Users.FirstOrDefault(u => u.Username == username);

            if (user.Password != HashPassword(password))
            {
                return null;
            }
            return user?.Id;
        }

        private static string HashPassword(string input) 
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
