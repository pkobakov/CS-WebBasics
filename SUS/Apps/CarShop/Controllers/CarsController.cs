namespace CarShop.Controllers
{
    using CarShop.Data.Models;
    using CarShop.Services;
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


        public HttpResponse Add() 
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("Users/Login");
            }

            var userId = this.GetUserId();
            var user = this.userService.GetUser(userId);

            if (user.IsMechanic)
            {
                return this.Error("Only Clients can add cars.");
            }

            return View();
        
        }


        [HttpPost]
        public HttpResponse Add(string model, int year, string image,string plateNumber) 
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("Users/Login");
            }

            var userId = this.GetUserId();
            var user = this.userService.GetUser(userId);

            if (user.IsMechanic)
            {
                return this.Error("Only Clients can add cars.");
            }
            if (string.IsNullOrWhiteSpace(model) || model.Length < 5 || model.Length > 20 )
            {
                return this.Error("Model name should be between 5 and 20 characters");
            }

            if (year < 0 || year < 2000 || year > 2055)
            {
                return this.Error("Invalid year.");
            }

            if (!Regex.IsMatch(plateNumber, @"^[A-Z]{2}[0-9]{4}[A-Z]{2}$"))
            {
                return this.Error("Invalid Plate number.");
            }

            var ownerId = this.GetUserId();
            this.carsService.CreateCar(model, year,image,plateNumber, ownerId);

            return Redirect("/Cars/All");
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }



            string userId = this.GetUserId();
            var cars = this.carsService.GetAll(userId);
            return this.View(cars);
        }



    }
}
