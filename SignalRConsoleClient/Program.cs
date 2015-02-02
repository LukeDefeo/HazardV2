using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using SignalR;
using SignalRConsoleHost;
using System.Linq;
using System.Collections.Generic;

namespace SignalRConsoleClient
{
    internal class Program
    {

        public class MyObserver : IObserver<string>
        {

            void IObserver<string>.OnCompleted()
            {
                Console.WriteLine("Observable Completed");
                
            }

            void IObserver<string>.OnError(Exception error)
            {
                Console.WriteLine("observable errored");
            }

            void IObserver<string>.OnNext(string value)
            {
                Console.WriteLine("Observing the message!! with strong typing");
            }
        }

        public delegate void MyAction(string message);

        private static void Main(string[] args)
        {
            var connection = new HubConnection(Constants.URL);

            var observableHubProxy = connection.CreateObservableHubProxy<IMyHub, IClient>("myHub");
            var observable = observableHubProxy.Observe<string>(client => client.AddMessage);
            var disposable = observable.Subscribe(new MyObserver());
            connection.Start().ContinueWith(task =>
                {
                    observableHubProxy.Call(hub => hub.Echo("Echo this back"));
                    disposable.Dispose();
                    Console.ReadKey();

                });

            Console.ReadKey();

          //  IHubProxy<IMyHub, IClient> hubProxy = connection.CreateHubProxy<IMyHub, IClient>("myHub");
          //  hubProxy.Observe<string>(client => client.AddMessage);
                                    //connection.SubscribeOn()
            //Make proxy to hub based on hub name on server
        //  IObservableHubProxy<> observableHubProxy = connection.CreateObservableHubProxy<IMyHub, IClient>("myHub");
       //   var list =   new List<int> {1,2,3};
           // list.Where()

           
         // var observable = observableHubProxy.Observe( client => 
          //{
           //   return Delegate.CreateDelegate(client,M);
//              return client.AddMessage(message);
          //});

            
            //connection.Start().ContinueWith(task =>
            //            {
            //                hubProxy.Call(hub => hub.Echo("Hello Wold"));
            //                Console.ReadKey();

            //            });
            //Console.ReadKey();



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

            
            myHub.On<Packet>("receivePacket", packet =>
            {
                Console.WriteLine("received packet {0} {1}", packet.IntProp, packet.StringProp);
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