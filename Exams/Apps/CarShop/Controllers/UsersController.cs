using CarShop.Services;
using CarShop.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        public HttpResponse Login() 
        {
            return this.View();
        
        }

        [HttpPost]
        public HttpResponse Login(string username, string password) 
        {
            var userId = this.userService.GetUserId(username, password);

            if (userId == null)
            {
                return this.Error("Invalid username or password");
            }

            this.SignIn(userId);
            return this.Redirect("/Cars/All");
        }


        public HttpResponse Register() 
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model) 
        {

            if (string.IsNullOrWhiteSpace(model.Username) || model.Username.Length<4 || model.Username.Length>20 )
            {
                return this.Error("Username should contain between 4 and 20 characters.");
            }

            if (!this.userService.IsUsernameAvailable(model.Username))
            {
                return this.Error("This username is already taken");
            }

            if (string.IsNullOrEmpty(model.Email) || !new EmailAddressAttribute().IsValid(model.Email))
            {
                return this.Error("Invalid email address.");
            }

            if (!this.userService.IsUserEmailAvilable(model.Email))
            {
                return this.Error("This email is already taken.");
            }

            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length<5 || model.Password.Length >20)
            {
                return this.Error("Password should contain between 5 and 20 characters.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Error("Password and ConfirmPassword should match.");
            }


           this.userService.Create(model.Username, model.Email, model.Password, model.UserType);

            return this.Redirect("/Users/Login");
        
        }
        public HttpResponse Logout() 
        {
            this.SignOut();
            return Redirect("/");
        
        }

    }
}
