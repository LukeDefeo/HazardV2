using System;
using Microsoft.AspNet.SignalR;

namespace SignalR
{
    public class MyHub : Hub, IMyHub
    {
        public void Echo(string name)
        {
            Console.WriteLine("Recieved Message");
            Clients.All.addMessage(name);
        }
    }
}