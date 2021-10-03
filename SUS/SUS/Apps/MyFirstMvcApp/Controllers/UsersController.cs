﻿namespace BattleCards.Controllers
{
    using BattleCards.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class UsersController : Controller
    {
        private UserService userService;

        public UsersController()
        {
            this.userService = new UserService();
        }


        public HttpResponse Login()
        {

            return this.View();

        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            var username = this.Request.FormData["username"];
            var password = this.Request.FormData["password"];
            var userId = this.userService.GetUserId(username, password);
            if (userId == null)
            {
                return this.Error("Invalid username or password");
            }

            this.SignIn(userId);
            return this.Redirect("/");

        }

        public HttpResponse Register()
        {

            return this.View();
        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister()
        {
            var username = this.Request.FormData["username"];
            var email = this.Request.FormData["email"];
            var password = this.Request.FormData["password"];
            var confirmPassword = this.Request.FormData["confirmPassword"];



            if (username == null || username.Length < 5 || username.Length > 20)
            {
                return this.Error("Invalid username. Username should be betwween 5 and 20 characters.");
            }

            if (!Regex.IsMatch(username, @"[a-zA-Z0-9\.]+"))
            {
                return this.Error("Invalid username format.");
            }

            if (!userService.IsUsernameAvailable(username))
            {
                return this.Error("This username is already taken");
            }

            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return this.Error("Invalid email");
            }

            if (!userService.IsEmailAvailable(email))
            {
                return this.Error("Email is already taken");
            }

            if (password == null || password.Length <6 || password.Length>20)
            {
                return this.Error("Invalid password. Password should be between 6 and 20 characters.");
            }

            if (password != confirmPassword)
            {
                return this.Error("Password and confirmPassword should match");
            }

            this.userService.CreateUser(username, email, password);
            return Redirect("/Users/Login");
        }

        public HttpResponse Logout() 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("Only logged in users can logout");

            }

            this.SignOut();
            return this.Redirect("/");

        }



    }
}
