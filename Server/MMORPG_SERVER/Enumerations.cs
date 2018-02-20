using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMORPG_SERVER
{
    class Enumerations
    {
        public enum ServerPackets
        {
            SAlertMsg = 1,
            SPlayerData,
            SPlayersMovement,
            SPlayerDisconnect

        }

        public enum ClientPackets
        {
            CNewAccount = 1,
            CHandleLogin,
            CHandleMovement,
            CHandleDisconnect
        }

    }
}
