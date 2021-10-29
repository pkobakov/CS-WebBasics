using Andreys.Data;
using Andreys.Data.Enums;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Andreys.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext data;

        public ProductsService(AndreysDbContext data)
        {
            this.data = data;
        }
        public void CreateProduct(AddProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Category = (Category)Enum.Parse(typeof(Category), model.Category,true),
                ImageUrl = model.ImageUrl,
                Gender = (Gender)Enum.Parse(typeof(Gender),model.Gender,true),
                Price = model.Price
            };

            this.data.Products.Add(product);
            this.data.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProduct(id);
            this.data.Remove(product);
            this.data.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        => this.data.Products.Select(p=> new Product
        { 
            Id = p.Id,
            Name = p.Name,
            Gender = p.Gender,
            ImageUrl = p.ImageUrl,
            Price = p.Price 
        }).ToList();

        public Product GetProduct(int id)
        {
            var product = this.data.Products.FirstOrDefault(p => p.Id == id);

            return product;
        }




    }
}
