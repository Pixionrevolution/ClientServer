using System;
using System.Threading;

namespace MMORPG_SERVER
{
    class Program
    {
        private static Thread threadConsole;
        private static bool consoleRunning;

        static void Main(string[] args)
        {
            threadConsole = new Thread(new ThreadStart(ConsoleThread));
            threadConsole.Start();
            Globals.networkHandleData.InitMessages();
            Globals.general.InitServer();
            Globals.gameLogic.ServerLoop();
        }

        private static void ConsoleThread()
        {
            string line;
            consoleRunning = true;

            while (consoleRunning)
            {
                line = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(line))
                {
                    consoleRunning = false;
                    return;
                }

                else
                {

                }
            }
        }
    }
}
