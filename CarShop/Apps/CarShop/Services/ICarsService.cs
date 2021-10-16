namespace CarShop.Services
{
    using CarShop.ViewModels.Cars;
    using System.Collections.Generic;
    public interface ICarsService
    {
        void CreateCar(string model,int year, string image, string plateNumber, string ownerId );
        IEnumerable<CarViewModel> GetAll(string ownerId);

    }
}
