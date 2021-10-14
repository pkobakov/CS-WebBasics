namespace CarShop.Controllers
{
    using CarShop.Data.Models;
    using CarShop.Services;
    using CarShop.ViewModels.Cars;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Text.RegularExpressions;

    public class CarsController : Controller
    {
        private readonly IUsersService userService;
        private readonly ICarsService carsService;

        public CarsController(IUsersService userService, ICarsService carsService)
        {
            this.userService = userService;
            this.carsService = carsService;
        }
        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }
            return this.View();
        }

        public HttpResponse Add() 
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("Users/Login");
            }

            return View();
        
        }
        [HttpPost]
        public HttpResponse Add(AddCarInputModel model) 
        {

            var userId = this.GetUserId();
            var user = this.userService.GetUser(userId);

            if (user.IsMechanic)
            {
                return this.Error("Only Clients can add cars.");
            }
            if (!Regex.IsMatch(model.PlateNumber, @"^[A-Z]{2}[0-9]{4}[A-Z]{2}$"))
            {
                return this.Error("Invalid Plate number.");
            }

            var ownerId = this.GetUserId();
            this.carsService.Create(ownerId, model.Model,model.Year, model.ImageUrl, model.PlateNumber);

            return Redirect("/Cars/All");
        }



    }
}
