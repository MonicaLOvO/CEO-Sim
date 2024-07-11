using CEO_simulator.MainLogic;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    /// <summary>
    /// Starting place of the program
    /// </summary>
    internal class Starting
    {
        private static GameLogic gameLogic = new GameLogic();

        public static void Main(string[] args)
        {
            ManageTool ManageTool = new ManageTool();
            //create event and item
            //ManageTool.start();

            //LoadData.SaveItem();
            //LoadData.SaveEvent();

            //start game
            gameLogic.Start();

            //Test Change

        }
    }
}