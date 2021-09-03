namespace MyFirstMvcApp
{
    using MyFirstMvcApp.Controllers;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Collections.Generic;
    using System.Threading.Tasks;


    class StartUp
    {
        static async Task Main(string[] args)
        {
            List<Route> routeTable = new List<Route>();
            routeTable.Add(new Route("/", new HomeController().Index));
            routeTable.Add(new Route("/Users/Login", new UsersController().Login));
            routeTable.Add(new Route("/Users/Register", new UsersController().Register));
            routeTable.Add(new Route("/Cards/Add", new CardsController().Add));
            routeTable.Add(new Route("/Cards/All", new CardsController().All));
            routeTable.Add(new Route("/Cards/Collection", new CardsController().Collection));


            routeTable.Add(new Route("/favicon.ico", new StaticFilesController().Favicon));
            routeTable.Add(new Route("/css/bootstrap.min.css", new StaticFilesController().BootstrapCss));
            routeTable.Add(new Route("/css/custom.css", new StaticFilesController().CustomCss));
            routeTable.Add(new Route("/js/bootstrap.bundle.min.js", new StaticFilesController().BootstrapJs));
            routeTable.Add(new Route("/js/custom.js", new StaticFilesController().CustomJs));

            await Host.CreateHostAsync(routeTable);
        }




    }
}
