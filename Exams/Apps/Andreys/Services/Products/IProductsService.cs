using Andreys.Data;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andreys.Services.Products
{
   public interface IProductsService
    {

        void CreateProduct(AddProductModel model);
        IEnumerable<Product> GetAll();
        Product GetProduct(int id);
    }
}
