using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Services
{
   public interface ICarsService
    {
        void Create(string ownerId,string model, int year, string imageUrl, string plateNumber );
    }
}
