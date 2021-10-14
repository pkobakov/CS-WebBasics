using CarShop.Data;
using CarShop.Data.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CarShop.Services
{
    public class UsersService: IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string username, string email, string password, string userType)
        {

            var user = new User 
            { 
            Username = username,
            Email = email,
            Password = HashPassword(password),
            IsMechanic = IsUserMechanic(userType)
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = this.db.Users.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);

            return user?.Id;
        }

        public bool IsUserMechanic(string usertype)
        {
            if (usertype == "Mechanic")
            {
                return true;
            }

            return false;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        public bool IsUserEmailAvilable(string email)
        {
            return !this.db.Users.Any(x=>x.Email == email);
        }

        public User GetUser(string userId) 
        {
            var user = this.db.Users.FirstOrDefault(x=>x.Id == userId);

            return user;
        
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
