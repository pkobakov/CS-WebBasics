namespace Andreys
{
    using SUS.MvcFramework;
    using System.Threading.Tasks;


    class Program
    {
        static async Task Main(string[] args)
        {


            await Host.CreateHostAsync(new StartUp());
        }
    }
}
