using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation
{
    internal class Player
    {
        //create attribute
        private double TotalMoneyValue;
        public double TotalMoney { get { return TotalMoneyValue; } set { TotalMoneyValue = Math.Round(value, 2); } }

        public int Reputation { get; set; }
        public int Day { get; set; }
        //constructor
        public Player() {
            Day = 0;
            Reputation = 15;
            TotalMoney = 1500;
        }
    }
}
