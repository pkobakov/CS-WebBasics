using CarShop.Data;
using CarShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Services
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext db;

        public CarsService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public void Create(string ownerId, string model, int year, string imageUrl, string plateNumber)
        {
            var car = new Car
            {
                OwnerId = ownerId,
                Model = model,
                Year = year,
                PictureUrl = imageUrl,
                PlateNumber = plateNumber

            };

            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }
    }
}
