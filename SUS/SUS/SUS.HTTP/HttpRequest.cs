namespace SUS.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using static HttpConstans;
    public class HttpRequest
    {
        public static IDictionary<string, Dictionary<string, string>> Sessions =
            new Dictionary<string, Dictionary<string, string>>();
        public HttpRequest(string requestString)
        {
            var lines = requestString.Split(new string[] { NewLine }, System.StringSplitOptions.None);
            this.Heathers = new List<Header>();
            this.Cookies = new List<Cookie>();
            this.FormData = new Dictionary<string, string>();
            this.QueryData = new Dictionary<string, string>();


            //GET /somepage HTTP/1.1
            var headerline = lines[0];
            var headerLineParts = headerline.Split(' ');
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLineParts[0], true);
            this.Path = headerLineParts[1];

            int lineIndex = 1;
            bool isHeader = true;
            StringBuilder bodyBuilder = new StringBuilder();

            while (lineIndex < lines.Length)
            {


                var line = lines[lineIndex];
                lineIndex++;
                if (string.IsNullOrEmpty(line))
                {
                    isHeader = false;
                    continue;
                }

                if (isHeader)
                {
                    Heathers.Add(new Header(line));
                }

                else
                {
                    bodyBuilder.AppendLine(line);
                }
            }

            if (this.Heathers.Any(h => h.Name == RequestCookieHeather))
            {
                var cookieAsString = this.Heathers.FirstOrDefault(h => h.Name == RequestCookieHeather).Value;
                var cookies = cookieAsString.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in cookies)
                {
                    this.Cookies.Add(new Cookie(cookie));
                }
            }

            var sessionCookie = this.Cookies.FirstOrDefault(x => x.Name == SessionName);
            if (sessionCookie == null)
            {
                var sessionId = Guid.NewGuid().ToString();
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionId, this.Session);
                this.Cookies.Add(new Cookie(SessionName, sessionId));
            }

            else if (!Sessions.ContainsKey(sessionCookie.Value))
            {

                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionCookie.Value, this.Session);
            }

            else
            {
                this.Session = Sessions[sessionCookie.Value];
            }

            if (Path.Contains("?"))
            {
                var pathParts = Path.Split(new char[] { '?' }, 2);
                this.Path = pathParts[0];
                this.QueryString = pathParts[1];
            }

            else
            {
                this.QueryString = string.Empty;
            }

            this.Body = bodyBuilder.ToString().TrimEnd('\n', '\r');
            SplitParameters(this.Body, this.FormData);
            SplitParameters(this.QueryString, this.QueryData);
        }

        private static void SplitParameters(string parametersAsString, IDictionary<string, string> output)
        {
            var parameters = parametersAsString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var parameter in parameters)
            {
                var paramParts = parameter.Split(new char[] { '=' }, 2);
                var name = paramParts[0];
                var value = WebUtility.UrlDecode(paramParts[1]);

                if (!output.ContainsKey(name))
                {
                    output.Add(name, value);
                }
            }
        }

        public string Path { get; set; }
        public string QueryString { get; set; }
        public HttpMethod Method  { get; set; }
        public string Body { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public ICollection<Header> Heathers { get; set; }
        public IDictionary<string, string> FormData { get; set; }
        public IDictionary<string, string> QueryData { get; set; }
        public Dictionary<string, string> Session { get; set; }

    }
}
