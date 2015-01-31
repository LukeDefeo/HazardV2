using System;
using Microsoft.Owin.Hosting;

namespace SignalRConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {

            using (WebApp.Start(Constants.URL))
            {
                Console.WriteLine("Server running on {0}", Constants.URL);
                Console.ReadLine();
            }
        }
    }
}