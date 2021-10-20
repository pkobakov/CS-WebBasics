using CarShop.Data.Models;

namespace CarShop.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        void Create(string username, string email, string password, string userType);

        User GetUser(string userId);
        bool IsUsernameAvailable(string username);

        bool IsUserEmailAvilable(string email);

        public bool IsUserMechanic(string Userid);
    }
}
