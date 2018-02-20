using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MMORPG_SERVER
{
    class GameLogic
    {
        Timer SavingPlayer;

        public void ServerLoop()
        {
            SavingPlayer = new Timer(SavePlayers,null,0,300000);
        }

        void SavePlayers(Object o)
        {
           for(int i  = 1; i< Constants.MAX_PLAYERS; i++)
            {
                if (Globals.general.IsPlaying(i))
                {
                    Globals.database.SavePlayer(i);
                }
            }
        }
    }
}
