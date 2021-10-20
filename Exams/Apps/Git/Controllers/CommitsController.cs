using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {

        private readonly IRepositoriesService repositoriesService;
        private readonly ICommitsService commitsService;

        public CommitsController(IRepositoriesService repositoriesService, ICommitsService commitsService)
        {
            this.repositoriesService = repositoriesService;
            this.commitsService = commitsService;
        }

        public HttpResponse Create(string repoId) 
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            var repoName = this.repositoriesService.GetRepositoryName(repoId);
            var commit = new CreateCommitViewModel 
            { 
              RepositoryId = repoId,
              RepositoryName = repoName            
            };

            return this.View(commit);
        
        }

        [HttpPost]
        public HttpResponse Create(string repoId, string description) 
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            if (description.Length < 5)
            {
                return this.Error("Description must have at least 5 characters.");
            }



            var userId = this.GetUserId();

            this.commitsService.CreateCommit(description, repoId, userId);

            return Redirect("/Repositories/All");
        }

        public HttpResponse All() 
        {

            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            var allcommits = this.commitsService.GetAll(userId);

            return this.View(allcommits);

        }

        public HttpResponse Delete(string id) 
        {
            if (!IsUserSignedIn())
            {
                this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (!this.commitsService.UserCanDeleteCommit(userId, id))
            {
                return this.Error("You are not authorized to delete this commit.");
            }

            this.commitsService.DeleteCommit(id);

            return this.Redirect("/Commits/All");
        }
    }
}
