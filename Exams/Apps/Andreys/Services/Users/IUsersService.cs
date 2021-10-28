using Andreys.Data;
using Andreys.ViewModels.Users;

namespace Andreys.Services.Users
{
   public interface IUsersService
    {
        void CreateUser(RegisterModel model);
        bool UsernameNotAvailable(string userId);
        bool EmailNotAvailable(string email);
        bool NotExistingUser(string userId);
        User GetUser(string username, string password);
    }
}
