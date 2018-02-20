using System;
using System.Net.Sockets;

namespace MMORPG_SERVER
{
    class Client
    {
        public int Index;
        public string IP;
        public Socket Socket;
        private byte[] readBuff = new byte[1024];

        public void Start()
        {       
            Socket.BeginReceive(readBuff, 0, readBuff.Length, SocketFlags.None, new AsyncCallback(OnReceiveData), Socket);
        }

        void CloseConnection()
        {

                Globals.networkSendData.SendDisconnect(Index, 0);
                Console.WriteLine("Player " + Index + " disconnected");
                Globals.tempPlayer[Index].inGame = false;
                Globals.database.SavePlayer(Index);
                Socket.Shutdown(SocketShutdown.Both);
                Socket.Close();
                Socket = null;
            
        }

        void OnReceiveData(IAsyncResult result)
        {
            Socket socket = (Socket)result.AsyncState;

            try
            {
                int received = socket.EndReceive(result);

                if (received <= 0)
                {
                   
                        CloseConnection();
                }
                else
                {
                    byte[] databuffer = new byte[received];
                    Array.Copy(readBuff, databuffer, received);
                    Globals.networkHandleData.HandleData(Index, databuffer);
                    Socket.BeginReceive(readBuff, 0, readBuff.Length, SocketFlags.None, new AsyncCallback(OnReceiveData), Socket);
                }
            }
            catch
            {
                if (Globals.Clients[Index].Index == Index)
                {
                    CloseConnection();
                }
            }
        }
    }
}
