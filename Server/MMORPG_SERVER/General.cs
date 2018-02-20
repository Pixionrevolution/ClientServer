using System;

namespace MMORPG_SERVER
{
    class General
    {
        public void InitServer()
        {
            Globals.mysql.MySQLInit();
            InitGameData();
            Globals.network.InitTCP();
        }

        public void InitGameData()
        {
            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                Globals.Clients[i] = new Client();
                Globals.tempPlayer[i] = new TempPlayer();
                Globals.player[i] = new Player();
            }
        }

        public bool IsPlaying(int index)
        {
            if (Globals.Clients[index] != null)
            {
                if(Globals.tempPlayer[index].inGame)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public float GetPlayerX(int index)
        {
            //prevent out of bounds
            if (index <= 0 || index > Constants.MAX_PLAYERS)
            {
                return 0;
            }
            return Globals.player[index].posX;
        }
        public float GetPlayerY(int index)
        {
            //prevent out of bounds
            if (index <= 0 || index > Constants.MAX_PLAYERS)
            {
                return 0;
            }
            return Globals.player[index].posY;
        }
        public float GetPlayerZ(int index)
        {
            //prevent out of bounds
            if (index <= 0 || index > Constants.MAX_PLAYERS)
            {
                return 0;
            }
            return Globals.player[index].posZ;
        }
        public void SetPlayerX(int index, float x)
        {
            if (index <= 0 || index > Constants.MAX_PLAYERS)
                return;

            Globals.player[index].posX = x;
        }
        public void SetPlayerY(int index, float y)
        {
            if (index <= 0 || index > Constants.MAX_PLAYERS)
                return;

            Globals.player[index].posY = y;
        }
        public void SetPlayerZ(int index, float z)
        {
            if (index <= 0 || index > Constants.MAX_PLAYERS)
                return;

            Globals.player[index].posZ = z;
        }
    }
}
