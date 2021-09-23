namespace MyFirstMvcApp.Controllers
{
    using MyFirstMvcApp.ViewModels;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System;


    public class HomeController:Controller
    {
        [HttpGet("/")]
        public HttpResponse Index (HttpRequest request)
        {
            var viewModel = new IndexViewModel();
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to the Battle Cards";
            return this.View(viewModel);

        }

        public HttpResponse About(HttpRequest request)
        {
            return View();
        }
    }
}
