using System;

namespace MMORPG_SERVER
{
    class Globals
    {
        public static Globals instance = new Globals();

        //Global instances of classes.
        public static General general = new General();
        public static Network network = new Network();
        public static Database database = new Database();
        public static MySQL mysql = new MySQL();
        public static NetworkHandleData networkHandleData = new NetworkHandleData();
        public static NetworkSendData networkSendData = new NetworkSendData();
        public static GameLogic gameLogic = new GameLogic();

        // Global Array instances of classes.
        public static TempPlayer[] tempPlayer = new TempPlayer[Constants.MAX_PLAYERS];
        public static Client[] Clients = new Client[Constants.MAX_PLAYERS];
        public static Player[] player = new Player[Constants.MAX_PLAYERS];

        //
        public int Player_HighIndex;
    }
}
