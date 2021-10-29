namespace Andreys.App.Controllers
{
    using Andreys.Services.Products;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService )
        {
            this.productsService = productsService;
        }
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (!IsUserSignedIn())
            {
                return this.View();
            }

            else
            {
                return this.Redirect("/Home");
            }
            
        }

        [HttpGet("/Home")]
        public HttpResponse Home() 
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            else
            {
                var allProducts = this.productsService.GetAll();
                return this.View(allProducts);
            }

        
        }


    }
}
