namespace MyFirstMvcApp
{
    using MyFirstMvcApp.Controllers;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Collections.Generic;

    public class StartUp : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {

            routeTable.Add(new Route("/", HttpMethod.GET, new HomeController().Index));
            routeTable.Add(new Route("/Users/Login", HttpMethod.GET, new UsersController().Login));
            routeTable.Add(new Route("/Users/Login", HttpMethod.POST, new UsersController().DoLogin));
            routeTable.Add(new Route("/Users/Register", HttpMethod.GET, new UsersController().Register));
            routeTable.Add(new Route("/Cards/Add", HttpMethod.GET, new CardsController().Add));
            routeTable.Add(new Route("/Cards/All", HttpMethod.GET, new CardsController().All));
            routeTable.Add(new Route("/Cards/Collection", HttpMethod.GET, new CardsController().Collection));


            routeTable.Add(new Route("/favicon.ico", HttpMethod.GET, new StaticFilesController().Favicon));
            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethod.GET, new StaticFilesController().BootstrapCss));
            routeTable.Add(new Route("/css/custom.css", HttpMethod.GET, new StaticFilesController().CustomCss));
            routeTable.Add(new Route("/js/bootstrap.bundle.min.js", HttpMethod.GET, new StaticFilesController().BootstrapJs));
            routeTable.Add(new Route("/js/custom.js", HttpMethod.GET, new StaticFilesController().CustomJs));
        }

        public void ConfigureServices()
        {

        }
    }
}
