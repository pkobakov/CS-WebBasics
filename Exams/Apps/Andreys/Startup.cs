namespace Andreys
{
    using Andreys.Data;
    using Andreys.Services.Products;
    using Andreys.Services.Users;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Collections.Generic;

    public class StartUp : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new AndreysDbContext().Database.EnsureCreated();
        }

        public void ConfigureServices()
        {

        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProductsService, ProductsService>();
        }
    }
}
