namespace Git.Controllers
{
    using Git.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;


    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Register() 
        {

            return View();
        }

        [HttpPost]
        public HttpResponse Register (string username, string email, string password, string confirmPassword)
        {

            if (string.IsNullOrEmpty(username) || username.Length < 5 || username.Length > 20)
            {
                return this.Error("Username should have between 5 and 20 characters.");
            }

            if (!Regex.IsMatch(username, @"[a-zA-Z0-9\.]+"))
            {
                return this.Error("Invalid username format.");
            }

            if (!this.usersService.IsUsernameAvailable(username))
            {
                return this.Error("This username is already taken.");
            }

            if (string.IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return this.Error("Invalid email");
            }

            if (!this.usersService.IsEmailAvailable(email))
            {
                return this.Error("This email is already taken");
            }

            if (password == null || password.Length < 6 || password.Length > 20)
            {
                return this.Error("Password should have between 6 and 20 characters.");
            }

            if (password != confirmPassword)
            {
                return this.Error("Password and Confirm Password should match.");
            }

            this.usersService.CreateUser(username, email, password);

            return Redirect("/Users/Login");
        
        }

        public HttpResponse Login() 
        {
            return View();
        
        }

        [HttpPost]
        public HttpResponse Login(string username, string password) 
        {


            var user = this.usersService.GetUserId(username, password);
            if (user == null)
            {
                return this.Error("Invalid username or password.");
            }

            this.SignIn(user);
            return this.Redirect("/");
        }

        public HttpResponse Logout() 
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            this.SignOut();
            return Redirect("/");
        
        }
    }
}
