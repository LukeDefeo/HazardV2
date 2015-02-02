namespace SignalR
{
    public interface IClient
    {
        void Something();
        void AddMessage(string message);
        void ReceivePacket(Packet packet);
    }
}