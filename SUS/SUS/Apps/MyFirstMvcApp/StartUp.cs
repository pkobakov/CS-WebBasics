namespace MyFirstMvcApp
{
    using SUS.HTTP;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    
    class StartUp
    {
        static async Task Main(string[] args)
        {
            var server = new HttpServer();
            
            server.AddRoute("/", HomePage);
            server.AddRoute("/favicon.ico", Favicon);
            server.AddRoute("/About", About);
            server.AddRoute("/Users/Login", Login);
            server.AddRoute("/Users/Register", Register);

            await server.StartAsync(80);
        }
        static HttpResponse HomePage (HttpRequest request) 
        {

            var responseHtml = "<h3>Welcome to the Jungle!</h3>" +
            request.Heathers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);


            return response;

        }
        static HttpResponse Favicon(HttpRequest request) 
        {
            var fileBytes = File.ReadAllBytes("wwwroot/favicon.ico");
            var response = new HttpResponse("image/vnd.microsoft.icon", fileBytes);
            return response;
        }
        static HttpResponse About (HttpRequest request)
        {
            var responseHtml = "About page";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/plain", responseBodyBytes);
            return response;
        }
        static HttpResponse Login (HttpRequest request)
        {

            var responseHtml = "Login page";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/plain", responseBodyBytes);
            return response;

        }

        static HttpResponse Register(HttpRequest request) 
        {
            var responseHtml = "Register page";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/plain", responseBodyBytes);
            return response;

            return response;
        }

    }
}
