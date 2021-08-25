using System;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Threading;

namespace HTTPClientDemo
{
    public class StartUp
    {
        const string Newline = "\r\n";
        static async Task Main(string[] args)
        {

            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 12345);
            tcpListener.Start();

            while (true)
            {
                var client = tcpListener.AcceptTcpClient();

                ProcessClientAsync(client);

            }

        }

        public static async Task ProcessClientAsync(TcpClient client)
        {
            using (var stream = client.GetStream())
            {


                byte[] buffer = new byte[1000000];
                var length = await stream.ReadAsync(buffer, 0, buffer.Length);

                string requestString = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                Console.WriteLine(requestString);


                string html = $"<a>Hello from PepiServer {DateTime.Now}</a>" +
                    $"<form method=post><input name=username /><input name=password />" +
                    $"<input type=submit /></form>";

                string response = "HTTP/1.1 200 OK" + Newline +
                    "Server: PepiServer 2021" + Newline +
                    //"Location: https://www.aninamontanari.com" + Newline +
                    "Set-Cookie: sid=myFirstCookie" + Newline +
                     "Set-Cookie: sid2=mySecondCookie" + Newline +
                    "Content-Type: text/html; charset= utf-8" + Newline +
                    "Content-Length:" + html + Newline +
                    Newline + html + Newline;

                byte[] reponseBytes = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(reponseBytes);

                Console.WriteLine(new string('=', 50));

            }
        }

        private static async Task ReadData()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string url = "https://softuni.bg/trainings/3164/csharp-web-basics-september-2020";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            Console.WriteLine("Status code: " + response.StatusCode);
            Console.WriteLine(string.Join(Environment.NewLine, response.Headers.Select(h => h.Key + ": " + h.Value.FirstOrDefault())));
        }
    }
}
