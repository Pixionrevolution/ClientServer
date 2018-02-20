using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMORPG_SERVER
{
    class NetworkSendData
    {
        public void SendDataTo(int index, byte[] data)
        {

            byte[] sizeinfo = new byte[4];
            sizeinfo[0] = (byte)data.Length;
            sizeinfo[1] = (byte)(data.Length >> 8);
            sizeinfo[2] = (byte)(data.Length >> 16);
            sizeinfo[3] = (byte)(data.Length >> 24);
            Globals.Clients[index].Socket.Send(sizeinfo);
            Globals.Clients[index].Socket.Send(data);
            
        }

        public void SendDataToAll(byte[] data)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.tempPlayer[i].inGame == true)
                {
                    SendDataTo(i, data);
                    Console.WriteLine(" SEND DATA TO : " + i);
                    System.Threading.Thread.Sleep(200);
                }

               
            }
            
        }

        public void SendDataToAllBut(int index, byte[] data)
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.Clients[i].Socket != null)
                {
                    if (i != index)
                    {
                        SendDataTo(i, data);
                    }
                }
            }
        }

        public byte[] PlayerData(int index)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();

            // prevent out of bounds
            if (index > Constants.MAX_PLAYERS)
                return null;

            buffer.WriteInteger((int)Enumerations.ServerPackets.SPlayerData);
            buffer.WriteInteger(index);
            buffer.WriteFloat(Globals.general.GetPlayerX(index));
            buffer.WriteFloat(Globals.general.GetPlayerY(index));
            buffer.WriteFloat(Globals.general.GetPlayerZ(index));

            buffer.WriteString(Globals.player[index].Username);
            return buffer.ToArray();
        }

        public void SendAlertMsg(int index, string alertMsg)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteInteger((int)Enumerations.ServerPackets.SAlertMsg);
            buffer.WriteString(alertMsg);

            SendDataTo(index, buffer.ToArray());
        }

        public /*async*/ void SendInGame(int index)
        {
            // Send player data to everyone including himself
            SendDataToAll(PlayerData(index));
            System.Threading.Thread.Sleep(200);
            // send all players to the player himself
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if (Globals.general.IsPlaying(i))
                {
                    if (i != index)
                    {
                        //await Task.Delay(75);
                        SendDataTo(index, PlayerData(i));
                        System.Threading.Thread.Sleep(20);

                    }

                }
               
            }
            Console.WriteLine("Player : '" + Globals.player[index].Username + "' has started playing");
        }

        public void SendPlayerMovement(int index, float x, float y, float z, float rotX, float rotY, float rotZ, float rotW)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteInteger((int)Enumerations.ServerPackets.SPlayersMovement);

            //Player Info

            buffer.WriteInteger(index);

            // Player Position

            buffer.WriteFloat(x);
            buffer.WriteFloat(y);
            buffer.WriteFloat(z);

            buffer.WriteFloat(rotX);
            buffer.WriteFloat(rotY);
            buffer.WriteFloat(rotZ);
            buffer.WriteFloat(rotW);

            SendDataToAllBut(index, buffer.ToArray());
        }


        public  void SendDisconnect(int index,int connected)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteInteger((int)Enumerations.ServerPackets.SPlayerDisconnect);
            buffer.WriteInteger(connected);
            buffer.WriteInteger(index);
            SendDataToAll(buffer.ToArray());
        }
    }
}
