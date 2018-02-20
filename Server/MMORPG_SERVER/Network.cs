using System;
using System.Net;
using System.Net.Sockets;

namespace MMORPG_SERVER
{
    class Network
    {

        private Socket _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] _buffer = new byte[1024];

        public void InitTCP()
        {     
            _server.Bind(new IPEndPoint(IPAddress.Any, 5500));
            _server.Listen(100);
            _server.BeginAccept(new AsyncCallback(OnClientConnect), null);
            Console.WriteLine("Server has successfully started.");
        }

        void OnClientConnect(IAsyncResult result)
        {         
            Socket socket = _server.EndAccept(result);
            _server.BeginAccept(new AsyncCallback(OnClientConnect), null);
            Console.WriteLine(" NewClient ") ;
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.Clients[i].Socket == null)
                {  
                    Globals.Clients[i].Socket = socket;
                    Globals.Clients[i].Index = i;
                    Globals.Clients[i].IP = socket.RemoteEndPoint.ToString();
                    Globals.Clients[i].Start();
                    Console.WriteLine("Incoming Connection from " + Globals.Clients[i].IP + "|| Index: " + i);
                    
                    return;
                }
            }
        }
    }
}
