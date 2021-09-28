namespace BattleCards.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System;

    public class UsersController: Controller
    {
      

        public HttpResponse Login()
        {

            return this.View();

        }

        [HttpPost]
        public HttpResponse DoLogin()
        {
            return this.Redirect("/");

        }

        public HttpResponse Register()
        {

            return this.View();
        }

    }
}
