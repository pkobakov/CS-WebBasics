namespace Andreys.Services.Users
{
    using Andreys.Data;
    using Andreys.ViewModels.Users;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;


    public class UsersService : IUsersService
    {
        private readonly AndreysDbContext data;

        public UsersService(AndreysDbContext data)
        {
            this.data = data;
        }
        public void CreateUser(RegisterModel model)
        {
            var user = new User 
            { 
            Username = model.Username,
            Email = model.Email,
            Password = HashPassword(model.Password)
            };

            this.data.Users.Add(user);
            this.data.SaveChanges();
        }

        public bool UsernameNotAvailable(string username)
        => this.data.Users.Any(u=>u.Username == username);

        public bool EmailNotAvailable(string email)
        => this.data.Users.Any(u => u.Email == email);

        public bool NotExistingUser(string username)
        {
            var user = this.data.Users.Where(u => u.Username == username).FirstOrDefault();

            if (user != null)
            {
                return false;
            }

            return true;

        }

        public User GetUser(string username, string password)
        => this.data.Users.Where(u => u.Username == username && u.Password == HashPassword(password)).FirstOrDefault();
        
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
