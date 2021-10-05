namespace SUS.MvcFramework
{
    using SUS.HTTP;
    using SUS.MvcFramework.ViewEngine;
    using System.Runtime.CompilerServices;
    using System.Text;

    public abstract class Controller
    {
        private const string UserSessionName = "UserId";
        private SusViewEngine viewEngine;

        public Controller()
        {
            this.viewEngine = new SusViewEngine();
        }

        public HttpRequest Request { get; set; }
        protected HttpResponse View(object viewModel = null, [CallerMemberName] string viewName = null)
        {


            var viewContent = System.IO.File.ReadAllText("Views/" +
                                                          this.GetType().Name.Replace("Controller", string.Empty) +
                                                          "/" +
                                                          viewName +
                                                          ".cshtml");

            viewContent = this.viewEngine.GetHtml(viewContent, viewModel, this.GetUserId());

            var responseHtml = PutViewInLayout(viewContent, viewModel);
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }



        public HttpResponse Redirect(string url)
        {

            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location", url));
            return response;

        }



        protected HttpResponse File(string filePath, string contentType)
        {

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contentType, fileBytes);
            return response;
        }

        protected HttpResponse Error(string errorMessage) 
        {
            var viewContent = $"<div class=\"alert alert-danger\" role=\"alert\">{errorMessage}</ div > ";
            var responseHtml = PutViewInLayout(viewContent);
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes, HttpStatusCode.ServerError);
            return response;
        
        }

        protected void SignIn(string userId) 
        {
            this.Request.Session[UserSessionName] = userId;
        }

        protected void SignOut() 
        {
            this.Request.Session[UserSessionName] = null;
        }

        protected bool IsUserSignedIn()
            => this.Request.Session.ContainsKey(UserSessionName) &&
            this.Request.Session[UserSessionName] != null;

        protected string GetUserId() => this.Request.Session
                                       .ContainsKey(UserSessionName)? this.Request.Session[UserSessionName]
                                                                    : null;


        private string PutViewInLayout(string viewContent, object viewModel = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");
            layout = layout.Replace("@RenderBody()", "___VIEWS_GOES_HERE___");
            layout = this.viewEngine.GetHtml(layout, viewModel, this.GetUserId());
            var responseHtml = layout.Replace("___VIEWS_GOES_HERE___", viewContent);
            return responseHtml;
        }
    }
}
