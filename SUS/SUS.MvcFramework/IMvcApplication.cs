namespace SUS.MvcFramework
{
    using SUS.HTTP;
    using System.Collections.Generic;


    public interface IMvcApplication
    {
        void ConfigureServices(IServiceCollection serviceCollection);
        void Configure(List<Route> routeTable);

    }
}
