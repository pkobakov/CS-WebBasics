namespace BattleCards
{
    using BattleCards.Data;
    using BattleCards.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Collections.Generic;

    public class StartUp : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.EnsureCreated();
        }

        public void ConfigureServices()
        {
            
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
        }
    }
}
