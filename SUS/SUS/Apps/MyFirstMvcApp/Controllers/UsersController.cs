namespace MyFirstMvcApp.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System;

    public class UsersController: Controller
    {
      

        public HttpResponse Login(HttpRequest request)
        {

            return this.View();

        }

        [HttpPost]
        public HttpResponse DoLogin(HttpRequest request)
        {
            return this.Redirect("/");

        }

        public HttpResponse Register(HttpRequest request)
        {

            return this.View();
        }

    }
}
