using CarShop.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;
        private readonly IUsersService usersService;

        public IssuesController(IIssuesService issuesService, IUsersService usersService)
        {
            this.issuesService = issuesService;
            this.usersService = usersService;
        }
        public HttpResponse Add(string carId) 
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("Users/Login");
            }

            return this.View(carId);
        }

        [HttpPost]
        public HttpResponse Add(string description, string carId) 
        {

            if (!this.IsUserSignedIn())
            {
                this.Redirect("Users/Login");
            }

            if (string.IsNullOrEmpty(description) || description.Length < 5)
            {
                return this.Error("Description is required");
            }

            if (!this.issuesService.CarIdIsValid(carId))
            {
                return this.Error($"There is no car with Id {carId}");
            }

            this.issuesService.AddIssue(description, carId);

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse CarIssues(string carId) 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("Users/Login");
            }

            var issues = this.issuesService.GetAll(carId);

            return this.View(issues);
        }

        public HttpResponse Fix(string issueId, string carId) 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("Users/Login");
            }

            var userId = this.GetUserId();

            if (this.usersService.IsUserMechanic(userId))
            {
                return this.Error("Only mechanics can fix issues");
            }

            this.issuesService.FixIssue(issueId, carId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");

        }

        public HttpResponse Delete(string ussueId, string carId) 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.issuesService.DeleteIssue(ussueId, carId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }


    }
}
