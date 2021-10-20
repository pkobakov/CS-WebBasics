namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Cars;
    using System.Collections.Generic;
    using System.Linq;


    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public void CreateCar(string model,int year, string image, string plateNumber, string ownerId)
        {
            
            var car = new Car
            {
                Model = model,
                Year = year,
                PictureUrl = image,
                PlateNumber = plateNumber,
                OwnerId = ownerId,
                Owner = this.db.Users.FirstOrDefault(x=>x.Id == ownerId)

            };

            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }

        public IEnumerable<CarViewModel> GetAll(string userId)
        {
            var cars = new List<CarViewModel>();
            var user = this.db.Users.FirstOrDefault(u => u.Id == userId);

            if (user.IsMechanic)
            {
                cars = this.db.Cars.Where(c => c.Issues.Any(i => !i.IsFixed))
                    .Select(c => new CarViewModel 
                    {
                        Id = c.Id,
                        Model = c.Model,
                        Year = c.Year,
                        Image = c.PictureUrl,
                        PlateNumber = c.PlateNumber,
                        RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count(),
                        FixedIssues = c.Issues.Where(i => i.IsFixed).Count()


                    }).ToList();
            }

            else
            {

                cars = this.db.Cars
               .Where(c => c.OwnerId == userId)
               .Select(c => new CarViewModel
               {
                   Id = c.Id,
                   Model = c.Model,
                   Year = c.Year,
                   Image = c.PictureUrl,
                   PlateNumber = c.PlateNumber,
                   RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count(),
                   FixedIssues = c.Issues.Where(i => i.IsFixed).Count()

               }).ToList();

            }

            return cars;
        }
    }
}
