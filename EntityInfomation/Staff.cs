using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation
{
    internal class Staff
    {
        //create attribute
        //possible value of staff 
        private int MAX_VAL = 100;
        private int MIN_VAL = -25;
        public string StaffName { get; set; }
        public double StaffValueActual { get; set; }
        public double StaffValueDefault { get; set; }
        //constructor
        public Staff() {
            StaffName = GenerateNewName();
            Random rnd = new Random();
            //random a Value
            StaffValueDefault = GenerateValue();
        }
        /// <summary>
        /// method to create a random name
        /// </summary>
        /// <returns></returns>
        public string GenerateNewName()
        {
            var letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            Random rnd = new Random();
            //random name lenth
            int ranLenth = rnd.Next(3, 5);
            string newName = "";

            
            for (int i = 0; i < ranLenth; i++)
            {
                //get a random letter add it to name string
                int ranChar = rnd.Next(0, letters.Length);
                char letter = letters[ranChar];
                if (i == 0)
                {
                    //capital
                    newName += letter.ToString().ToUpper();

                }
                else
                {
                    newName += letter;
                }
            }
            return newName;
        }
        public int GenerateValue() {

            Random rnd = new Random();


            return rnd.Next(MIN_VAL, MAX_VAL);
        }
    }
}
