using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.ViewModels.Cars
{
    public class AddCarInputModel
    {
        public string Model { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public string PlateNumber { get; set; }

    }
}
