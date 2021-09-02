namespace MyFirstMvcApp
{
    using MyFirstMvcApp.Controllers;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    class StartUp
    {
        static async Task Main(string[] args)
        {
            List<Route> routeTable = new List<Route>();
            routeTable.Add(new Route("/", new HomeController().Index));
            routeTable.Add(new Route("/favicon.ico", new StaticFilesController().Favicon));
            routeTable.Add(new Route("/Users/Login", new UsersController().Login));
            routeTable.Add(new Route("/Users/Register", new UsersController().Register));
            routeTable.Add(new Route("/Cards/Add", new CardsController().Add));
            routeTable.Add(new Route("/Cards/All", new CardsController().Add));
            routeTable.Add(new Route("/Cards/Collection", new CardsController().Collection));

            await Host.CreateHostAsync(routeTable);
        }




    }
}
