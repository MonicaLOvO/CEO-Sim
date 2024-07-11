using CEO_simulator.EntityInfomation.EffectFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation
{
    internal class ShopItem
    {

        //create attribute
        public string ItemName { get; set; }

        public double ItemPrice { get; set; }
        public string ItemDescription { get; set; }
        public Effect Effect { get; set; }


    }
}
