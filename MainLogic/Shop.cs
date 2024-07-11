using CEO_simulator.EntityInfomation;
using CEO_simulator.EntityInfomation.EffectFolder;
using CEO_simulator.EntityInfomation.EffectFolder.ActualEffects;
using CEO_simulator.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.MainLogic
{
    internal class Shop
    {
        public List<ShopItem> ItemList = new List<ShopItem>();
        public List<ShopItem> tempItemList = new List<ShopItem>();
        public int TEMP_ITEM_NUMBER = 3;

        public void toShop()
        {
            //clean screen
            CleanScreen.Clean();
            RefreshShop();


            Console.WriteLine($"\t |_| \t\t |_| \t\t |_| \t   ^---^      ");
            Console.WriteLine($"\t |1| \t\t |2| \t\t |3| \t ( - v - )  n ");
            Console.WriteLine($"\t(___)\t\t(___)\t\t(___)\t(  u   u  )// ");
            //Console.WriteLine($"\t{tempItemList[0].ItemName}\t{tempItemList[1].ItemName}\t{tempItemList[2].ItemName}");



            Console.WriteLine("\ndo you want to buy something? ");
            Console.WriteLine($"TotalMoney: {GameLogic.player.TotalMoney} Rep:{GameLogic.player.Reputation}\n");
            Console.WriteLine("0) Exit\n");

           //print item in temp list
            for (int i = 0; i < TEMP_ITEM_NUMBER; i++)
            {
                Console.WriteLine($"{i+1}) Item Name:{tempItemList[i].ItemName}   Item Price: {tempItemList[i].ItemPrice}");
                Console.WriteLine($"Item Description: {tempItemList[i].ItemDescription}\n");


            }
            int chose = InputService.TakeInt(0, TEMP_ITEM_NUMBER);
           

            if (chose != 0)
            {
                //tempItemList[i].
                
                if (GameLogic.player.TotalMoney < tempItemList[chose - 1].ItemPrice)
                {
                    Console.WriteLine($"You don't have enough money");
                    CleanScreen.Clean(2);
                }
                else
                {
                    InstantEffect temp = new InstantEffect();
                    temp.MoneyChange = new MoneyChange();
                    //set chosen item's price as negative number to subtruct it from total money
                    temp.MoneyChange.Value = 0 - tempItemList[chose - 1].ItemPrice;

                    //proccess of item price
                    GameLogic.effectLogic.ProccessOptionEffect(temp);
                    //proccess of item effect
                    GameLogic.effectLogic.ProccessOptionEffect(tempItemList[chose - 1].Effect);
                    Console.WriteLine($"Effect added");
                    CleanScreen.Clean(2);
                }
                
            }


        }
        public void RefreshShop() {
            //clear the temp list
            tempItemList.Clear();
            Random rnd = new Random();

            //random 3 item and add them to temp list 
            for (int i =0; i< TEMP_ITEM_NUMBER; i++) {
                int ranItem = rnd.Next(0, ItemList.Count);
                tempItemList.Add(ItemList[ranItem]);
            }

        }
    }
}
