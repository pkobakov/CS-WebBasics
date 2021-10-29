namespace Andreys.Controllers
{
    using Andreys.Services.Products;
    using Andreys.ViewModels.Products;
    using SUS.HTTP;
    using SUS.MvcFramework;


    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Details(int id) 
        {

            var product = this.productsService.GetProduct(id);

            var details = new DetailsModel 
            { 
                Id = id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Category = product.Category,
                Gender = product.Gender
            };

            return this.View(details);
        
        }

        public HttpResponse Add() => this.View();

        [HttpPost]
        public HttpResponse Add(AddProductModel model) 
        {
            if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Product name should have between 4 and 20 characters.");
            }

            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > 10)
            {
                return this.Error("Product description can not be more than 10 characters.");
            }

            if (model.ImageUrl == null)
            {
                return this.Error("Image url is required.");
            }

            this.productsService.CreateProduct(model);
            return this.Redirect("/Home");
        }

        public HttpResponse Delete(int id) 
        {

            this.productsService.DeleteProduct(id);
            return this.Redirect("/Home");
        }

    }
}
