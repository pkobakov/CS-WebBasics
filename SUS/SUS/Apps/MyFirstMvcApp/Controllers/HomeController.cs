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
            var viewModel = new IndexViewModel();
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to the Battle Cards";
            return this.View(viewModel);

        }

        public HttpResponse About()
        {
            return View();
        }
    }
}
