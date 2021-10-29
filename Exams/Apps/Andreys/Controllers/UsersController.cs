namespace Andreys.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Net.Mail;
    using Andreys.Services.Users;
    using Andreys.ViewModels.Users;
    using SUS.HTTP;
    using SUS.MvcFramework;


    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Register() => this.View();
        [HttpPost]
        public HttpResponse Register(RegisterModel model) 
        {
            if (string.IsNullOrEmpty(model.Username) || model.Username.Length < 4 || model.Username.Length >10)
            {
                return this.Error("Username should contain between 4 and 10 characters.");
            }

            if (this.usersService.UsernameNotAvailable(model.Username))
            {
                this.Error("Username is already taken.");
            }

            if (string.IsNullOrWhiteSpace(model.Email) || !new EmailAddressAttribute().IsValid(model.Email))
            {
                return this.Error("Invalid email.");
            }

            if (this.usersService.EmailNotAvailable(model.Email))
            {
                return this.Error("Email is already taken.");
            }

            if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace (model.ConfirmPassword))
            {
                return this.Error("Password and Confirm Password are required.");
            }

            if (model.ConfirmPassword != model.Password)
            {
                return this.Error("Password and Confirm Password should match.");
            }

            this.usersService.CreateUser(model);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Login() => this.View();

        [HttpPost]
        public HttpResponse Login(LoginModel model) 
        {
            var user = usersService.GetUser(model.Username, model.Password);

            if (user == null )
            {
                return this.Error("Incorrect username or password.");
            }

            this.SignIn(user.Id);

            return this.Redirect("/Home");
        
        }

        public HttpResponse Logout() 
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
