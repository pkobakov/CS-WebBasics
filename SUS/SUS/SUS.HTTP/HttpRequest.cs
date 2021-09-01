namespace SUS.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using static HttpConstans;
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            var lines = requestString.Split(new string[] { NewLine }, System.StringSplitOptions.None);
            this.Heathers = new List<Header>();
            this.Cookies = new List<Cookie>();

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

            if (this.Heathers.Any(h=>h.Name == RequestCookieHeather))
            {
                var cookieAsString = this.Heathers.FirstOrDefault(h => h.Name == RequestCookieHeather).Value;
                var cookies = cookieAsString.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in cookies)
                {
                    this.Cookies.Add(new Cookie(cookie));
                }
            }
            this.Body = bodyBuilder.ToString();
        }


        public string Path { get; set; }
        public HttpMethod Method  { get; set; }
        public string Body { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
        public ICollection<Header> Heathers { get; set; }
    }
}
