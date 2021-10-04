using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCards.Services
{
    public interface IUserService
    {
        string CreateUser(string username, string email,string password);
        string GetUserId(string username, string password);
        bool IsUsernameAvailable(string username);
        bool IsEmailAvailable(string email);
    }
}
