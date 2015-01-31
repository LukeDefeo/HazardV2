using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using SignalR;
using SignalRConsoleHost;

namespace SignalRConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connection = new HubConnection(Constants.URL);


            IHubProxy<IMyHub, IClient> hubProxy = connection.CreateHubProxy<IMyHub, IClient>("myHub");
            //                        connection.SubscribeOn()
            //Make proxy to hub based on hub name on server
            var observableHubProxy = connection.CreateObservableHubProxy<IMyHub, IClient>("myHub");
//            var observable = observableHubProxy.Observe();
            
            connection.Start().ContinueWith(task =>
                        {
                            hubProxy.Call(hub => hub.Echo("Hello Wold"));
                            Console.ReadKey();

                        });
            Console.ReadKey();



            //            Task.Run(async () =>
            //            {
            //
            //
            //
            //            }).Wait();

            Console.WriteLine("Starting connection");
            //Set connection
            var myHub = connection.CreateHubProxy("MyHub");
            //Start connection

            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                    Console.ReadKey();
                    return;

                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            myHub.On<string>("addMessage", param =>
            {
                Console.WriteLine(param);
            });

            myHub.Invoke<string>("Echo", "HELLO World ").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error calling send: {0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine(task.Result);
                }
            });



            //            myHub.Invoke<string>("DoSomething", "I'm doing something!!!").Wait();


            Console.Read();
            connection.Stop();



        }
    }
}