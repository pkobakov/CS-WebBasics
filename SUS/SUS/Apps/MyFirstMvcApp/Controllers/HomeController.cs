namespace MyFirstMvcApp.Controllers
{
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Linq;
    using System.Text;


    public class HomeController:Controller
    {
        public HttpResponse Index (HttpRequest request)
        {

            return this.View();

        }
    
    }
}
