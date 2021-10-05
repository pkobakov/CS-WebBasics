namespace SUS.MvcFramework
{
    
    using SUS.HTTP;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication application, int port = 80) 
        {
            List<Route> routeTable = new List<Route>();
            IServiceCollection serviceCollection = new ServiceCollection();

            AutoRegisterStaticFiles(routeTable);
            AutoRegisterRoutes(routeTable, application, serviceCollection);
            
            application.ConfigureServices(serviceCollection);
            application.Configure(routeTable);
            var server = new HttpServer(routeTable);
            await server.StartAsync(port);

        }

        private static void AutoRegisterRoutes(List<Route> routeTable, IMvcApplication application, IServiceCollection serviceCollection)
        {
           var controllerTypes =  application
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

                    routeTable.Add(new Route(url, httpMethod, request=>ExecuteAction(request, controller, method, serviceCollection)));
                }
            }
        }

        private static HttpResponse ExecuteAction(HttpRequest request, Type controller, MethodInfo action, IServiceCollection serviceCollection)
        {

                var instance = serviceCollection.CreateInstance(controller) as Controller;
                instance.Request = request;
                var arguments = new List<object>();

                var parameters = action.GetParameters();

                foreach (var parameter in parameters)
                {
                    var parameterValue = GetParameterFromRequest(request, parameter.Name);
                    arguments.Add(parameterValue);

                }

                var response = action.Invoke(instance, arguments.ToArray() ) as HttpResponse;

                return response;

            
        }

        private static string GetParameterFromRequest(HttpRequest request, string parameter) 
        {

            if (request.FormData.ContainsKey(parameter)) 
            {
                return request.FormData[parameter];
            
            }

            return null;
        
        
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
