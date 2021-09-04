namespace SUS.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Net;
    using System.Threading.Tasks;
    using System.Text;

    using static HttpConstans;
    using System.Linq;

    public class HttpServer : IHttpServer
    {
        List<Route> routeTable;
        public HttpServer(List<Route> routeTable)
        {
            this.routeTable = routeTable;
        }


        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                await ProcessClientAsync(tcpClient);
            }


        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    List<byte> data = new List<byte>();
                    byte[] buffer = new byte[BufferSize];
                    int position = 0;

                    while (true)
                    {
                        int count = await stream.ReadAsync(buffer, position, buffer.Length);
                        position += count;
                        if (count < buffer.Length)
                        {
                            var partialBuffer = new byte[count];

                            Array.Copy(buffer, partialBuffer, count);
                            data.AddRange(partialBuffer);
                            break;
                        }
                        else
                        {
                            data.AddRange(buffer);
                        }
                    }
                    var requestAsString = Encoding.UTF8.GetString(data.ToArray());
                    var request = new HttpRequest(requestAsString);
                    Console.WriteLine($"{request.Method} {request.Path} => {request.Heathers.Count}" );

                    HttpResponse response;
                    var route = this.routeTable.FirstOrDefault(r => string.Compare(r.Path,request.Path) == 0 
                                                                               && r.Method == request.Method);

                    if (route != null)
                    {
                      
                        response = route.Action(request);
                    }

                    else
                    {
                        response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                    }


                    response.Headers.Add(new Header("Server", "SUS Server 1.0"));
                    response.Cookies.Add(new ResponseCookie("sid", Guid.NewGuid().ToString())
                    {
                        HttpOnly = true,
                        MaxAge = 60 * 24 * 60 * 60
                    });

                    var responseHeadersBytes = Encoding.UTF8.GetBytes(response.ToString());

                    await stream.WriteAsync(responseHeadersBytes, 0, responseHeadersBytes.Length);
                    await stream.WriteAsync(response.Body, 0, response.Body.Length);

                }
                tcpClient.Close();
            }
            catch (Exception message)
            {

                Console.WriteLine(message);
            }

        }
    }
}
