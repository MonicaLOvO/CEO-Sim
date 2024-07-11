using CEO_simulator.EntityInfomation;
using CEO_simulator.EntityInfomation.EffectFolder;
using CEO_simulator.EntityInfomation.EffectFolder.ActualEffects;
using CEO_simulator.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.MainLogic
{
    internal class LoadData
    {
        //the type wanted when convert Json
        private static JsonSerializerSettings INCLUDE_TYPE = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        /// <summary>
        /// example of saving event in json
        /// </summary>
        public static void SaveEvent()
        {
            var eventList = new List<Event>();

            var newEvent = new Event();
            newEvent.EventName = "This is EventName";

            newEvent.EventTypeList.Add(EventType.morningEvent);
            newEvent.EventTypeList.Add(EventType.noonEvent);
            newEvent.EventTypeList.Add(EventType.nightEvent);
            newEvent.EventText = "This is EventText";

            var newOption = new Option() {
                Effects = new List<Effect>(),
                MoneyRequirement = 0,
                ReputationRequirement = 0,
                OptionText = "This is  option Text",
            };

            var instantEffect = new InstantEffect {
                IsDuration = false,
                OptionResult = "This is  event Result",
                Weight = 5,
                MoneyChange = new MoneyChange
                {
                    calculationType = CalculationType.addtion,
                    Value = 0

                },
                ReputationChange = new ReputationChange
                {
                    calculationType = CalculationType.multipulication,
                    Value = 0

                },

                StaffChange = new StaffChange {
                    EffectStaffAmount = 2,
                    calculationType = CalculationType.addtion

                }

            };

            var EndEffect = new EndDurationEffect
            {
                Duration = 2,
                IsEndEffect= true,
                IsDuration = true,
                OptionResult = "This is  event Result",
                Weight = 3,
                MoneyChange = new MoneyChange
                {
                    calculationType = CalculationType.multipulication,
                    Value = 0

                },
                ReputationChange = new ReputationChange
                {
                    calculationType = CalculationType.addtion,
                    Value = 0

                },

                StaffChange = new StaffChange
                {
                    
                    EffectStaffAmount = 2,
                    calculationType = CalculationType.abilityChange,
                    AdditionAbility = 2

                }

            };

            var InstantDurationEffect = new InstantDurationEffect
            {
                Duration = 3,
                IsEndEffect = false,
                IsDuration = true,
                OptionResult = "This is  event Result",
                Weight = 5,
                MoneyChange = new MoneyChange
                {
                    calculationType = CalculationType.multipulication,
                    Value = 0

                },
                ReputationChange = new ReputationChange
                {
                    calculationType = CalculationType.addtion,
                    Value = 0

                },

            };

            
            newOption.Effects.Add(instantEffect);
            newOption.Effects.Add(EndEffect);
            newOption.Effects.Add(InstantDurationEffect);


            newEvent.OptionList.Add(newOption);
            eventList.Add(newEvent);

            //convert object into data(string) 
            var data = JsonConvert.SerializeObject(eventList, INCLUDE_TYPE);
            //write file to Event.jason
            FileService.WriteFile("Event.json", data);
        }
        /// <summary>
        /// example of saving item in jason 
        /// </summary>
        public static void SaveItem()
        {
            var eventList = new List<ShopItem>();

            var newItem = new ShopItem();

            newItem.ItemDescription= "ItemDescription";
            newItem.ItemName = "Item";

            newItem.ItemPrice = 0;

            var EndEffect = new EndDurationEffect
            {
                Duration = 2,
                IsEndEffect = true,
                IsDuration = true,
                OptionResult = "This is  event Result",
                Weight = 3,
                MoneyChange = new MoneyChange
                {
                    calculationType = CalculationType.multipulication,
                    Value = 0

                },
                ReputationChange = new ReputationChange
                {
                    calculationType = CalculationType.addtion,
                    Value = 0

                },

                StaffChange = new StaffChange
                {

                    EffectStaffAmount = 2,
                    calculationType = CalculationType.abilityChange,
                    AdditionAbility = 2

                }
            };

            newItem.Effect = EndEffect;
            eventList.Add(newItem);

            var data = JsonConvert.SerializeObject(eventList, INCLUDE_TYPE);

            FileService.WriteFile("Item.json", data);
        }
        /// <summary>
        /// temp use to build JSON file with correct fomate
        /// </summary>
        /// <param name="shopList"></param>
        public static void SaveData(List<ShopItem> shopList) 
        {
            //convert json to data(string)
            var data = JsonConvert.SerializeObject(shopList, INCLUDE_TYPE);
            //convert back to json
            FileService.WriteFile("Item.json", data);
        }
        /// <summary>
        /// temp use to build JSON file with correct fomate
        /// </summary>
        /// <param name="eventList"></param>
        public static void SaveData(List<Event> eventList)
        {
            var data = JsonConvert.SerializeObject(eventList, INCLUDE_TYPE);

            FileService.WriteFile("Event.json", data);
        }
        /// <summary>
        /// read item infor from json
        /// </summary>
        /// <returns></returns>
        public static List<ShopItem> LoadItem()
        {
            //read rawData(string) from Item.json
            var rawData = FileService.ReadFile("Item.json");
            //return objects converted from data by JsonConvet class
            return JsonConvert.DeserializeObject<List<ShopItem>>(rawData, INCLUDE_TYPE) ?? new List<ShopItem>();
            //return JsonConvert.DeserializeObject<List<ShopItem>>(rawData);
        }
        /// <summary>
        /// read event infor from json
        /// </summary>
        /// <returns></returns>
        public static List<Event> LoadEvent()
        {
            //read data(string) from Event.json
            var rawData = FileService.ReadFile("Event.json");
            
            //return objects converted from data by JsonConvet class
            return JsonConvert.DeserializeObject<List<Event>>(rawData, INCLUDE_TYPE) ?? new List<Event>();
            //return JsonConvert.DeserializeObject<List<Event>>(rawData);

            //foreach (var item in eventList)
            //{
            //    Console.WriteLine(item.EventText);
            //    foreach(var option in item.OptionList)
            //    {
            //        Console.WriteLine(option.MoneyEffect);
            //    }
            //}
        }
    }
}
