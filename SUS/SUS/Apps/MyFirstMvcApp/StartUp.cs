namespace MyFirstMvcApp
{
    using BattleCards.Data;
    using MyFirstMvcApp.Controllers;
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
    }
}
