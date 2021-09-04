using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeNumbersCounter
{
    class StartUp
    {
       static int Count = 0;
       static object lockObj = new object(); 
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            PrintPrimeCount(1, 10_000_000);
            Console.WriteLine(sw.Elapsed);

            //race condition:
           //List<int> numbers = Enumerable.Range(0, 10_000).ToList();
           
           //for (int i = 0; i < 4; i++)
           //{
           //    new Thread(() => 
           //    {
           //        while (numbers.Count>0)
           //        {
           //            numbers.RemoveAt(numbers.Count - 1);
           //        }
           //    }).Start();
           //
           //}

            // Threads:

            // thread initialization:

            //Thread thread2 = new Thread(() => 
            //{ 
            //
            //   code....
            //
            //});

           //Thread thread2 = new Thread(()=>PrintPrimeCount(2_500_001, 5_000_000));
           //Thread thread1 = new Thread(()=>PrintPrimeCount(1,2_500_000));
           //Thread thread3 = new Thread(()=>PrintPrimeCount(5_000_001, 7_500_000));
           //Thread thread4 = new Thread(()=>PrintPrimeCount(7_500_001, 10_000_000));
            
            
            //Stopwatch sw = Stopwatch.StartNew();
            //thread1.Start(); //starts the thread execution
            //thread2.Start();
            //thread3.Start();
            //thread4.Start();
            
            //thread1.Join();  //waits the execution of the thread to be finished
            //thread2.Join();
            //thread3.Join();
            //thread4.Join();
            //Console.WriteLine(Count);
            //Console.WriteLine(sw.Elapsed);
            
            //while (true)
            //{
            //    string input = Console.ReadLine();
            //    Console.WriteLine(input.ToUpper());
            //}

         // Tasks:

            //Stopwatch sw2 = Stopwatch.StartNew();
            //List<Task> tasks = new List<Task>();

            //for (int i = 1; i <= 100; i++)
            //{
            //    var task = Task.Run(async () => 
            //    {
            //        HttpClient httpClient = new HttpClient();
            //        var url = $"https://vicove.com/vic-{i}";
            //        var httpResponse = await httpClient.GetAsync(url);
            //        var vic = await httpResponse.Content.ReadAsStringAsync();
            //        Console.WriteLine(vic.Length);
            //    });
            //
            //    tasks.Add(task);
            //    Console.WriteLine(sw2.Elapsed);
            //}


        }




        private static void PrintPrimeCount(int min, int max)
        {

            //for (int i = min; i <= max; i++)
            Parallel.For(min, max + 1, i =>
           {
               bool isPrime = true;

               for (int j = 2; j <= Math.Sqrt(i); j++)
               {
                   if (i % j == 0)
                   {
                       isPrime = false;
                   }
               }

               if (isPrime)
               {
                   lock (lockObj)
                   {
                       Count++;
                   }

               }
           });

            Console.WriteLine(Count);
        }
    }
}
