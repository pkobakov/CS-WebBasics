namespace SUS.MvcFramework
{
    
    using SUS.HTTP;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication mvcApplication, int port = 80) 
        {
            List<Route> routeTable = new List<Route>();

            AutoRegisterStaticFiles(routeTable);
            AutoRegisterRoutes(routeTable, mvcApplication);
            
            mvcApplication.ConfigureServices();
            mvcApplication.Configure(routeTable);
            var server = new HttpServer(routeTable);
            await server.StartAsync(port);

        }

        private static void AutoRegisterRoutes(List<Route> routeTable, IMvcApplication mvcApplication)
        {
           var controllerTypes =  mvcApplication
                                 .GetType()
                                 .Assembly
                                 .GetTypes()
                                 .Where(x => x.IsClass && 
                                             x.IsSubclassOf(typeof(Controller)) &&
                                             !x.IsAbstract);

            foreach (var controller in controllerTypes)
            {
                var methods = controller
                                    .GetMethods()
                                    .Where(x=>!x.IsStatic &&
                                               x.IsPublic &&
                                               x.DeclaringType == controller &&
                                               !x.IsAbstract &&
                                               !x.IsConstructor && 
                                               !x.IsSpecialName);

                foreach (var method in methods)
                {
                    var url = "/" + 
                               controller.Name.Replace("Controller", string.Empty) +
                               "/" +
                               method.Name;
                  var attribute = method.GetCustomAttributes(false)
                          .Where(x=>x.GetType()
                                     .IsSubclassOf(typeof(BaseHttpAttribute)))
                                     .FirstOrDefault()as BaseHttpAttribute;

                    var httpMethod = HttpMethod.GET;

                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (!string.IsNullOrEmpty(attribute?.Url))
                    {
                        url = attribute.Url;
                    }

                    routeTable.Add(new Route(url, httpMethod, (request) =>
                   {
                       var instance = Activator.CreateInstance(controller) as Controller;
                       instance.Request = request;
                       var response = method.Invoke(instance, new object[] {}) as HttpResponse;

                       return response;
                   }));
                }
            }
        }

        private static void AutoRegisterStaticFiles(List<Route> routeTable) 
        {

            var staticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);

            foreach (var file in staticFiles)
            {
                var url = file
                         .Replace("wwwroot", string.Empty)
                         .Replace("\\", "/");

                routeTable.Add(new Route(url, HttpMethod.GET, (request) =>
                {
                    var fileContent = File.ReadAllBytes(file);
                    var fileExtension = new FileInfo(file).Extension;
                    var contentType = fileExtension switch
                    {
                        ".txt" => "text/plain",
                        ".js" => "text/javascript",
                        ".css" => "text/css",
                        ".jpg" => "image/jpg",
                        ".jpeg" => "image/jpg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        ".ico" => "image/vnd.microsoft.icon",
                        ".html" => "text/html",
                        _ => "text/plain"

                    };

                    return new HttpResponse(contentType, fileContent, HttpStatusCode.OK);
                }));

            }
        }
    }
}
