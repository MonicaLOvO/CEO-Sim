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
    internal class Office
    {
        //create attribute
        private const int COMPANY_UPGRADE_COST_MULTIPLYER = 200;
        private const int HIREING_COST = 100;
        private const int LAY_OFF_REPUTATION_COST = 5;
        private const int MAXMUMLEVEL = 3;

        /// <summary>
        /// work option
        /// </summary>
        /// <param name="targetType"></param>
        public void toOffice(EventType targetType)
        {
            CleanScreen.Clean();
            Console.WriteLine("1) work(work event)");
            Console.WriteLine("2) manage your Company");

            int chose = InputService.TakeInt(1, 2);
            if (chose == 1)
            {
                //work event parameter: time,   in company or not
                GameLogic.RunEvent(targetType, inCompany: true); 
            }
            else
            {
                //Calculate upgrade Cost
                double upgradeCost = COMPANY_UPGRADE_COST_MULTIPLYER * GameLogic.comp.Level;
                while (chose != 3) {
                    CleanScreen.Clean();
                    Console.WriteLine($"1) upgrade Company(cost: {Math.Round(upgradeCost, 2)}, maxmum level = {MAXMUMLEVEL}(effect your maxmum space for staffs))");
                    Console.WriteLine("2) manage your staffs");
                    Console.WriteLine("3) Exit");
                    //Console.WriteLine("3) change Company strategy");
                    chose = InputService.TakeInt(1, 3);
                    if (chose == 1)
                    {
                        if (GameLogic.comp.Level == 3)
                        {
                            Console.WriteLine("your company is full level.");
                            CleanScreen.PressClean();
                        }
                        else
                        {
                            GameLogic.comp.Level += 1;
                            GameLogic.player.TotalMoney -= upgradeCost;
                            Console.WriteLine("you have upgraded your company");
                            Console.WriteLine($"Total money left: ${GameLogic.player.TotalMoney}");
                            Console.WriteLine($"your company is now level: {GameLogic.comp.Level}");
                            CleanScreen.PressClean();
                        }

                    }
                    else if (chose == 2)
                    {
                        ManageStaff();
                        
                    }


                }

            }
        }
        public void ManageStaff()
        {
            int chose = 0;
            while (chose != 3)
            {
                CleanScreen.Clean();
                //print all staffs
                PrintStaffs();

                Console.WriteLine("what do you want to do?");
                Console.WriteLine($"1) hire staffs(maxmum {10 * GameLogic.comp.Level} staffs)");
                Console.WriteLine("2) lay off staffs");
                Console.WriteLine("3) Exit");
                chose = InputService.TakeInt(1, 3);

                if (chose == 1)
                {

                    HireStaff();
                    CleanScreen.Clean();
                }
                else if (chose == 2)
                {
                    FireStaff();
                    CleanScreen.Clean();
                }

            }


        }
        public void FireStaff() {
            CleanScreen.Clean();
            PrintStaffs();

            Console.WriteLine($"which staff do you want to fire?(cost {LAY_OFF_REPUTATION_COST} reputation)");
            //take input between 1 to the number of the total staffs
            int chose = InputService.TakeInt(1, GameLogic.comp.StaffList.Count);

            //set the staff that will be fire as target staff
            var targetStaff = GameLogic.comp.StaffList[chose - 1];
            Console.WriteLine($"You have fired {targetStaff.StaffName}");
            //remove the targetStaff from staff list in company
            GameLogic.comp.removeStaff(targetStaff);
            GameLogic.player.Reputation -= LAY_OFF_REPUTATION_COST;

            Console.WriteLine($"Total Reputation left: {GameLogic.player.Reputation}");
            Console.WriteLine($"\n");
            //wait 5s and clean the screen
            CleanScreen.PressClean();

        }
        public void HireStaff()
        {
            CleanScreen.Clean();
            //variable to check 
            bool again = true;
            //set the max number of staff can be in the company
            int space = 10 * GameLogic.comp.Level;
            while (again)
            {
                PrintStaffs();
                Console.WriteLine($"how many staffs you want to hire?");
                Console.WriteLine($"(cost {HIREING_COST} each, you have {space - GameLogic.comp.StaffList.Count} space and ${GameLogic.player.TotalMoney} left, 0 to EXSIT)\n");
                //take input of number from 0 to the space left
                int chose = InputService.TakeInt(0, space - GameLogic.comp.StaffList.Count);
                int totalCost = HIREING_COST * chose;
                //if TotalMoney is not enough to pay the hire cost
                if (GameLogic.player.TotalMoney < totalCost)
                {
                    Console.WriteLine($"You don't have enough money to hire {chose} staffs.");
                    CleanScreen.PressClean();
                }
                else
                {
                    //add (chose)number of staff to stafflist
                    GameLogic.comp.addStaff(chose);

                    InstantEffect temp = new InstantEffect();
                    temp.MoneyChange = new MoneyChange();
                    temp.MoneyChange.Value = 0 - totalCost;
                    GameLogic.effectLogic.ProccessOptionEffect(temp);
                   

                    Console.WriteLine($"Total money left: ${GameLogic.player.TotalMoney}");
                    Console.WriteLine($"\n");
                    //if hired then set again as false
                    again = false;
                    CleanScreen.PressClean();
                }
            }
        }
        /// <summary>
        /// prin staffs in the list
        /// </summary>
        public void PrintStaffs()
        {

            Console.WriteLine($"Staff List");
            Console.WriteLine($"Stuff Number\tStuff Name\tStuff Value");
            //if stafflist is empty
            if (GameLogic.comp.StaffList.Count == 0)
            {
                Console.WriteLine($"\t*No staff avaliable");
            }
            //else loop through the StaffList and print infor
            for (int i = 0; i < GameLogic.comp.StaffList.Count; i++)
            {
                Console.WriteLine($"{i + 1})\t\t{GameLogic.comp.StaffList[i].StaffName}\t\t{GameLogic.comp.StaffList[i].StaffValueDefault}\t$/Day");

            }
            //if stafflist is not empty print total trunover and player's money and Rep
            if (GameLogic.comp.StaffList.Count != 0)
            {
                //                                                          sum the StaffValu in the list
                Console.WriteLine($"Total $/Day: {GameLogic.comp.StaffList.Sum(staff => staff.StaffValueDefault)}");
                Console.WriteLine($"Total money left: ${GameLogic.player.TotalMoney}");
                Console.WriteLine($"Total Rep left: {GameLogic.player.Reputation}");
            }
            
            Console.WriteLine($"\n");
        }





















    }
}
