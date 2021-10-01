namespace BattleCards.Controllers
{
    using BattleCards.ViewModels;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System;


    public class HomeController:Controller
    {
        [HttpGet("/")]
        public HttpResponse Index ()
        {
            if (this.IsUserSignedIn())
            {
                return Redirect("/Cards/All");
            }
            return this.View();

        }

        public HttpResponse About()
        {

            this.SignIn("pepi");
            return this.View();
        }
    }
}
