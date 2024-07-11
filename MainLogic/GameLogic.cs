using CEO_simulator.EntityInfomation;
using CEO_simulator.EntityInfomation.EffectFolder;
using CEO_simulator.Service;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.MainLogic
{
    internal class GameLogic
    {
        //create attribute
        internal static List<Event> eventList = new List<Event>();
        internal static Player player = new Player();
        internal static Company comp = new Company();
        private Office officelogic = new Office();
        private Shop shoplogic = new Shop();
        public static EffectLogic effectLogic = new EffectLogic();
        

        internal static int DAYS_MAXMUM = 12;

        public void Start()
        {
            //load items in file to the itemlist
            shoplogic.ItemList = LoadData.LoadItem();
            //load Event in file to the Eventlist
            eventList = LoadData.LoadEvent();


            if (SaveFileService.LoadGame()==false) {
                CleanScreen.Clean();
                Console.WriteLine("Hello player!\nYou are the child of the richest person in the world, he gave you 30 days to manage a new company he just handed to you.");
                Console.WriteLine("At the end of the 12 days, he will evaluate your ability to become the CEO.\nIf you do not have the talent, he might kick you out.");
                Console.WriteLine("But in another way, he will value you if your result satisfies him. (you will be evaluate by your money and reputation)");
                Console.WriteLine("\nPress enter to start");
                Console.ReadLine();

            }




            for (; player.Day < DAYS_MAXMUM; player.Day++)
            {
                SaveFileService.SaveNewJson();
                CleanScreen.Clean();
                Console.WriteLine($"*Day {player.Day + 1}\n");
                Console.WriteLine($"What a nice beginning of the day! do you want to save your game now? (maximum 10)");
                Console.WriteLine($"1) yes");
                Console.WriteLine($"2) no");
                int saveOrNo = InputService.TakeInt(1, 2);
                //save file
                if (saveOrNo == 1) {
                    SaveFileService.SaveNewJson(false);
                }

                //set variable from time
                EventType time;
                
                for (int t=0;t<3;t++) {
                    CleanScreen.Clean();
                    //morning
                    if (t == 0) {
                        //set time as morning
                        time = EventType.morningEvent;
                        Console.WriteLine($"*Day {player.Day+1}\n");
                        Console.WriteLine("/// Morning ///");
                        Console.WriteLine("What do you want to do for the Morning?");
                        Console.WriteLine($"TotalMoney: {player.TotalMoney} Rep:{player.Reputation}");
                        Menu(time);
                        
                    }
                    //afternoon
                    else if (t==1) {
                        //set time as noon
                        time = EventType.noonEvent;
                        Console.WriteLine($"*Day {player.Day + 1}\n");
                        Console.WriteLine("/// Afternoon ///");
                        Console.WriteLine("Do you want to do something in the Afternoon?");
                        Console.WriteLine($"TotalMoney: {player.TotalMoney} Rep:{player.Reputation}");
                        Menu(time);
                        
                    }
                    //night
                    else {
                        //set time as night
                        time = EventType.nightEvent;
                        Console.WriteLine($"*Day {player.Day + 1}\n");
                        Console.WriteLine("/// night ///");
                        Console.WriteLine("Is there anything you want to do at night?");
                        Console.WriteLine($"TotalMoney: {player.TotalMoney} Rep:{player.Reputation}");
                        Menu(time);
                       
                    }

                }
                //method work at the end of a day
                EndDay();

            }
            //LoadData.LoadEvent();
            //LoadData.SeeEvent();
            //method work at the end of the game
            End();
        }

        /// <summary>
        /// print walk event
        /// </summary>
        /// <param name="targetType"></param>
        public void Adventure(EventType targetType) {
            //run a event
            RunEvent(targetType, inCompany: false);
        }

        public void Menu(EventType time) {
            //print menu
            Console.WriteLine("");
            Console.WriteLine("1) go to the company");
            Console.WriteLine("2) walk around");
            Console.WriteLine("3) buy something");
            Console.WriteLine("4) do nothing");

            int chose = InputService.TakeInt(1, 4);
            //if go to company
            if (chose == 1)
            {
                officelogic.toOffice(time);
                CleanScreen.Clean();
            }//if walk around
            else if (chose == 2)
            {
                Adventure(time);
                CleanScreen.Clean();
            }//if go to shop
            else if (chose == 3)
            {
                shoplogic.toShop();
                CleanScreen.Clean();
            }
        }
        /// <summary>
        /// get a list of option that fulfill the option requirement
        /// </summary>
        /// <param name="newEvent"></param>
        /// <returns></returns>
        public static List<Option> GetAvaliableOptionList(Event newEvent) {
            List<Option> tempOptionList = new List<Option>();
            //for each option in the OptionList
            foreach (var option in newEvent.OptionList)
            {
                //if the option do not have Requirement 
                if (option.ReputationRequirement == null || option.MoneyRequirement == null)
                {
                    //add option to the new list
                    tempOptionList.Add(option);
                }
                //if the option Requirement fulfill
                else if (player.Reputation >= option.ReputationRequirement && player.TotalMoney >= option.MoneyRequirement)
                {
                    //add option to the new list
                    tempOptionList.Add(option);
                }
            }
            //return the new list
            return tempOptionList;

        }
        /// <summary>
        /// run event
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="inCompany"></param>
        public static void RunEvent(EventType targetType, bool inCompany) {
            //get a rendon event from the method
            var newEvent = GetRandomEvent(targetType, inCompany);
            //print event text
            Console.WriteLine(newEvent.EventText);
            //get option that fulfill the requirement
            var avaliableOptionList = GetAvaliableOptionList(newEvent);
            int i = 1;
            //for each option in the option list
            foreach (var option in avaliableOptionList)
            {
                //prin Option Text
                Console.WriteLine($"{i}) {option.OptionText}");
                i++;
            }
            //take input
            int choice = InputService.TakeInt(1, avaliableOptionList.Count);
            //get the chosen option
            var optionChoosed = avaliableOptionList[choice - 1];

            var targetEffect = optionChoosed.GetRandomEffect();
            //print the result
            Console.WriteLine(targetEffect.OptionResult);

            //proccess the effect
            effectLogic.ProccessOptionEffect(targetEffect);
            CleanScreen.PressClean();
        }
        /// <summary>
        /// random a event that meet the requirement
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="inCompany"></param>
        /// <returns></returns>
        public static Event GetRandomEvent(EventType targetType, bool inCompany) {
            //find all event that meet the requirement in the list
            var targetList = eventList.Where(e => e.InCompany == inCompany && e.EventTypeList.Contains(targetType)).ToList();

            Random rnd = new Random();
            int ranNum = rnd.Next(targetList.Count);

            //return a random event
            return targetList[ranNum];

            //Evernt targetEvent 
        }
        /// <summary>
        /// end of the game
        /// </summary>
        public void End() {
            CleanScreen.Clean();
            Console.WriteLine("it's time now");

            Console.WriteLine($"TotalMoney: {player.TotalMoney} Rep:{player.Reputation}");
            //all the ending requirement
            if (player.Reputation >= 90)
            {

                Console.WriteLine("Your father think you are too kind to be a CEO, decide to give you some money and left");
                Console.WriteLine("$TotalMoney: +1000000");
                Console.WriteLine("\n*achievement: Become a rich people");
            }
            else if (player.Reputation < 10)
            {

                Console.WriteLine("Your father see your Reputation, and think you are not someone he can trust, Decided to kick you out of the family");
                Console.WriteLine("\n*achievement: Become the untrustable people");

            }
            else if (player.TotalMoney >= 100000)
            {

                Console.WriteLine("Your father aprshiace your ability to become a rich CEO, diciede to give all his company to you");
                Console.WriteLine("company ++");
                Console.WriteLine("\n*achievement: Become the most CEO person");
            }

            else if (player.TotalMoney >= 20000 && player.Reputation >= 60)
            {

                Console.WriteLine("Your father aprshiace your ability, diciede to give all his money and company to you");
                Console.WriteLine("$TotalMoney: +100000000000...");
                Console.WriteLine("\n*achievement: Become the richest people");
            }
            else if (player.TotalMoney >= 7000 && player.Reputation >= 40)
            {
                Console.WriteLine("Your father satisfy with the result, Decided to teach you how to correctly run a company");
                Console.WriteLine("\n*achievement: Become a real CEO");

            }
            
            else if(player.TotalMoney < 500)
            {

                Console.WriteLine("Your father think your suck at manage a company, Decided to kick you out of the family");
                Console.WriteLine("\n*achievement: Become the Poorest people");

            }
            else if (player.TotalMoney < 3000 && player.Reputation < 25)
            {

                Console.WriteLine("Your father do not satisfy with the result, take the company away from you.");
                Console.WriteLine("\n*achievement: Become nobody");
            }
            else {
                Console.WriteLine("Your father see your result, Decided to let you keep the company");
                Console.WriteLine("\n*achievement: Become your own CEO");

            }

            Console.WriteLine("\n*End");

        }
        /// <summary>
        /// end of the day
        /// </summary>
        public void EndDay() {
            CleanScreen.Clean();
            Console.WriteLine("It's the end of the day");
            //Calculate the company Trunover of todays
            comp.CalculateTrunover();
            Console.WriteLine($"Your company turnover: {comp.Turnover}");

            //go through all the end day effect
            effectLogic.ProccessEndDayEffect(comp.Turnover);
            Console.WriteLine($"TotalMoney: {player.TotalMoney} Rep:{player.Reputation}");
            CleanScreen.PressClean();
        }
    }
}
