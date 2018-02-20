using System;
using System.Collections.Generic;

namespace MMORPG_SERVER
{
    class NetworkHandleData
    {
        private delegate void Packet_(int Index, byte[] Data);
        private Dictionary<int, Packet_> Packets;

        public void InitMessages()
        {
            Packets = new Dictionary<int, Packet_>();
            Packets.Add((int)Enumerations.ClientPackets.CNewAccount, HandleNewAccount);
            Packets.Add((int)Enumerations.ClientPackets.CHandleLogin, HandleLogin);
            Packets.Add((int)Enumerations.ClientPackets.CHandleMovement, HandleMovement);
        }

        public void HandleData(int index, byte[] data)
        {
            int packetnum;
            Packet_ Packet;
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteBytes(data);
            packetnum = buffer.ReadInteger();
            buffer = null;

            if (packetnum == 0)
                return;

            if (Packets.TryGetValue(packetnum, out Packet))
            {
                Packet.Invoke(index, data);
            }

        }

        void HandleNewAccount(int index, byte[]data)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteBytes(data);

            int packetnum = buffer.ReadInteger();
            string username = buffer.ReadString();
            string password = buffer.ReadString();
            Globals.database.AddAccount(username, password);
        }

        void HandleLogin(int index, byte[]data)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteBytes(data);

            int packetnum = buffer.ReadInteger();
            string username = buffer.ReadString();
            string password = buffer.ReadString();
            if(!Globals.database.AccountExists(index,username))
            {
                //SendUserDoesNotExist
                return;
            }
            if (!Globals.database.PasswordOK(index,username, password))
            {
                // Send password not correct
                return;
            }
            Console.WriteLine("Player" + username + " logged in successfully!");
            //Send Player into game
            //Load player
            Globals.database.LoadPlayer(index, username);
            Globals.tempPlayer[index].inGame = true;
            // Set tempPlayer.ingame = true;
            Globals.networkSendData.SendInGame(index);
        }

        void HandleMovement(int index, byte[]data)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteBytes(data);
            int packetnum = buffer.ReadInteger();
            float posX = buffer.ReadFloat();
            float posY = buffer.ReadFloat();
            float posZ = buffer.ReadFloat();

            float rotX = buffer.ReadFloat();
            float rotY = buffer.ReadFloat();
            float rotZ = buffer.ReadFloat();
            float rotW = buffer.ReadFloat();

            Globals.general.SetPlayerX(index, posX);
            Globals.general.SetPlayerY(index, posY);
            Globals.general.SetPlayerZ(index, posZ);

            // SendPlayer Movement
            Globals.networkSendData.SendPlayerMovement(index, posX, posY, posZ, rotX, rotY, rotZ, rotW);


        }
        void HandleDisconnect(int index, byte[] data)
        {
            KaymakGames.KaymakGames buffer = new KaymakGames.KaymakGames();
            buffer.WriteBytes(data);
            int packetnum = buffer.ReadInteger();

            int connected = buffer.ReadInteger();
            int Index = buffer.ReadInteger();
            Globals.tempPlayer[index].inGame = false;
            // Set tempPlayer.ingame = true;
            Globals.networkSendData.SendDisconnect(Index, connected);


        }
    }
}
