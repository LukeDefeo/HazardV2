using System;
using Microsoft.AspNet.SignalR;

namespace SignalR
{
    public class Packet
    {
        public int IntProp { get; set; }
        public string StringProp { get; set; }
    }

    //putting Iclient here as a generic screwed a lot up
    public class MyHub : Hub<IClient>, IMyHub
    {
        public void Echo(string name)
        {
            Console.WriteLine("Recieved Message");
            
            Clients.All.AddMessage(name);
            Clients.All.ReceivePacket(new Packet { IntProp = 2, StringProp = "hello" });
        }

    }
}