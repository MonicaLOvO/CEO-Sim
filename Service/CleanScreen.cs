using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.Service
{
    internal class CleanScreen
    {
        /// <summary>
        /// clean screen
        /// </summary>
        /// <param name="secondWait"></param>
        public static void Clean(int secondWait = 0) {
            //sleep(Wait for second*1000)
            Thread.Sleep(secondWait*1000);

            //loop 100 thimes of 'nextline'
            for (int i=0;i<100;i++) {
                Console.WriteLine("\n");

            }
        
        }
        /// <summary>
        /// press enter to clean the screen
        /// </summary>
        public static void PressClean()
        {
            Console.WriteLine("\nPress enter to continue");
            Console.ReadLine();
            //loop 100 thimes of 'nextline'
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("\n");

            }

        }

    }
}
