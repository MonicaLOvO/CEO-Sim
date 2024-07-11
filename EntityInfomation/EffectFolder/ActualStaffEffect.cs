using CEO_simulator.EntityInfomation.EffectFolder.ActualEffects;
using CEO_simulator.MainLogic;
using CEO_simulator.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation.EffectFolder
{
    internal class ActualStaffEffect
    {
        public static void ProccessStaffEffect(StaffChange StaffChange)
        {
            // if calculation type is addition
            if (StaffChange.calculationType == CalculationType.addtion)
            {
                int space = 10 * GameLogic.comp.Level;
                int leftSpace = space - GameLogic.comp.StaffList.Count;
                int staffNum = StaffChange.EffectStaffAmount;
               //if staff change is nigative number
                if (staffNum < 0)
                {
                    staffNum = 0 - staffNum;
                    for (int i=0; i < staffNum; i++) {
                        if (GameLogic.comp.StaffList.Count == 0)
                        {
                            Console.WriteLine("There is no Staffs left.");
                            break;
                        }
                        else {
                            //remove random staff from staff list
                            Random random = new Random();
                            int target = random.Next(0, GameLogic.comp.StaffList.Count);
                            Console.WriteLine($"{GameLogic.comp.StaffList[target].StaffName} lefted.");
                            GameLogic.comp.removeStaff(GameLogic.comp.StaffList[target]);

                        }
                    }

                }
                else
                {
                    if (StaffChange.EffectStaffAmount > leftSpace)
                    {
                        Console.WriteLine("There is no enough room for the staffs,external staffs are change to 50$ ");

                        int externalStaff = staffNum - leftSpace;
                        staffNum = leftSpace;

                        InstantEffect temp = new InstantEffect();
                        temp.MoneyChange = new MoneyChange();
                        temp.MoneyChange.Value = (50 * externalStaff);
                        GameLogic.effectLogic.ProccessOptionEffect(temp);

                        Console.WriteLine($"Total money: ${GameLogic.player.TotalMoney}");

                    }
                    GameLogic.comp.addStaff(staffNum);

                }

                CleanScreen.PressClean();

            }
            // if is ability change
            else if (StaffChange.calculationType == CalculationType.abilityChange)
            {
                if (GameLogic.comp.StaffList.Count == 0)
                {
                    Console.WriteLine("There is no Staffs in your Company.");

                }
                else {
                    //random a staff and add ability
                    int staffNum = StaffChange.EffectStaffAmount;
                    for (int i = 0; i < staffNum; i++)
                    {
                        Random random = new Random();
                        int target = random.Next(0, GameLogic.comp.StaffList.Count);

                        GameLogic.comp.StaffList[target].StaffValueDefault += StaffChange.AdditionAbility;
                        Console.WriteLine($"{GameLogic.comp.StaffList[target].StaffName} ability have changed. ability:{GameLogic.comp.StaffList[target].StaffValueDefault}");

                    }

                }
                

                CleanScreen.PressClean();


            }
            // if is random ability
            else if (StaffChange.calculationType == CalculationType.random)
            {
                if (GameLogic.comp.StaffList.Count == 0)
                {
                    Console.WriteLine("There is no Staffs in your Company.");
                    
                }
                else {
                    //random a staff and random his ability
                    int staffNum = StaffChange.EffectStaffAmount;
                    for (int i = 0; i < staffNum; i++)
                    {
                        Random random = new Random();
                        int target = random.Next(0, GameLogic.comp.StaffList.Count);

                        GameLogic.comp.StaffList[target].StaffValueDefault = GameLogic.comp.StaffList[target].GenerateValue();

                        Console.WriteLine($"{GameLogic.comp.StaffList[target].StaffName} ability have reseted. ability:{GameLogic.comp.StaffList[target].StaffValueDefault}");
                    }

                }
                CleanScreen.PressClean();

            }
        }

    }
}
