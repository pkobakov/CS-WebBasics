namespace Git.Controllers
{
    using Git.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;


    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All() 
        {


           var allRepos = this.repositoriesService.GetAll();

            return this.View(allRepos);
        }

        public HttpResponse Create() 
        {

            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return View();

        }

        [HttpPost]
        public HttpResponse Create(string name, string repositoryType) 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(name) || name.Length < 3 || name.Length > 10)
            {
                return this.Error("Name should have between 3 and 10 characters.");
            }


            var ownerId = this.GetUserId();
           this.repositoriesService.CreateRepository(name, repositoryType, ownerId);

           
            return this.Redirect("/Repositories/All");

        }


    }
}
