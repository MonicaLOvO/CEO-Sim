using CEO_simulator.EntityInfomation;
using CEO_simulator.EntityInfomation.EffectFolder;
using CEO_simulator.EntityInfomation.EffectFolder.ActualEffects;
using CEO_simulator.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.MainLogic
{
    /// <summary>
    /// new event and items creator
    /// </summary>
    internal class ManageTool
    {
        private List<Event> eventList = new List<Event>();
        public List<ShopItem> itemList = new List<ShopItem>();

        //the type wanted when convert Json
        private static JsonSerializerSettings INCLUDE_TYPE = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        /// <summary>
        /// new event and items creator
        /// </summary>
        public void start() {
            while (true) { 
                ReadFile();

                PrintEventList();
                Console.WriteLine("\n");
                PrintItemList();
                Console.WriteLine("\n");
                Console.WriteLine("0) exit");
                Console.WriteLine("1) add Event");
                Console.WriteLine("2) add Item");
                Console.WriteLine("3) edit Event");
                Console.WriteLine("4) edit Item");
                Console.WriteLine("5) delete Event");
                Console.WriteLine("6) delete Item");
                int chose = InputService.TakeInt();
                if (chose == 1)
                {
                    AddEvent();

                }
                else if (chose == 2)
                {
                    AddItem();
                }
                else if (chose == 3) {

                    EditEvent();


                } else if (chose == 4)
                {

                    EditItem();


                }
                else if (chose == 5)
                {

                    DeleteEvent();


                }
                else if (chose == 6)
                {

                    DeleteItem();


                }else if (chose == 0) { break; }
            }
        }

        public void DeleteEvent() {
            Console.WriteLine("Which Event do you want to delete");

            int index = InputService.TakeInt();


            eventList.Remove(eventList[index]);
            //convert object into data(string) 
            var data = JsonConvert.SerializeObject(eventList, INCLUDE_TYPE);
            //write file to Event.jason
            FileService.WriteFile("Event.json", data);
        }

        public void DeleteItem()
        {
            Console.WriteLine("Which item do you want to delete");

            int index = InputService.TakeInt();


            itemList.Remove(itemList[index]);
            //convert object into data(string) 
            var data = JsonConvert.SerializeObject(itemList, INCLUDE_TYPE);
            //write file to Event.jason
            FileService.WriteFile("Item.json", data);
        }

        public void ReadFile() {

            //load items in file to the itemlist
             itemList = LoadData.LoadItem();
            //load Event in file to the Eventlist
            eventList = LoadData.LoadEvent();
    }


        public void EditEvent() {

            Console.WriteLine("Which Event do you want to edit");

            var targetEvent = new Event();
            int index = InputService.TakeInt();
            targetEvent = eventList[index];

            PrintOption(index);

            var newOption = new Option() { Effects = new List<Effect>() };
            JObject newContainerJson = new JObject();

            Console.WriteLine("What do you want to change ");
            Console.WriteLine("1) option");
            Console.WriteLine("2) Effect");
            int chose = InputService.TakeInt();

            if (chose == 1)
            {
                Console.WriteLine("how many option do you want to add");
                int optionAns = InputService.TakeInt();

                for (int i = 0; i < optionAns; i++)
                {
                    newOption = SetOption();
                    

                    Console.WriteLine("how many effect do you want to have");
                    int effectNum = InputService.TakeInt();

                    for (int j = 0; j < effectNum; j++)
                    {
                        newOption.Effects.Add(EffectCreator());

                    }
                    targetEvent.OptionList.Add(newOption);

                }

            }
            else {
                Console.WriteLine("Which option do you want to edit effect");
                Option targetOption = new Option();
                int targetChose = InputService.TakeInt();

                targetOption = targetEvent.OptionList[targetChose];

                targetOption.Effects.Add (EffectCreator());

            }
           

            


            eventList[index] = targetEvent;
            //convert object into data(string) 
            var data = JsonConvert.SerializeObject(eventList, INCLUDE_TYPE);
            //write file to Event.jason
            FileService.WriteFile("Event.json", data);

        }

        public void EditItem() {
            Console.WriteLine("Which Item do you want to edit");

            var targetItem = new ShopItem();
            int index = InputService.TakeInt();
            targetItem = itemList[index];

            Console.WriteLine("do you want to change effect or Item Information");
            Console.WriteLine("1) effect");

            int chose = InputService.TakeInt();

            

            Effect Effect = new Effect();
            JObject newContainerJson = new JObject();


                Effect = EffectCreator(true);
                targetItem.Effect = Effect;

            


            itemList[index] = targetItem;
            //convert object into data(string) 
            var data = JsonConvert.SerializeObject(itemList, INCLUDE_TYPE);
            //write file to Event.jason
            FileService.WriteFile("Item.json", data);
        }

        public void PrintOption(int index) {

            Console.WriteLine($"\n{eventList[index].EventName}\n");

            for (int i = 0; i < eventList[index].OptionList.Count; i++)
            {

                Console.WriteLine($"{i}){eventList[index].OptionList[i].OptionText}");

            }
            Console.WriteLine($"\n\n");
        }

        public void PrintItemList() {
            Console.WriteLine("\n");
            Console.WriteLine("Printing ItemList");

            for (int i = 0; i< itemList.Count; i++) {

                Console.WriteLine($"{i}){itemList[i].ItemName}");

            }

        }

        public void PrintEventList()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Printing EventName");

            for (int i = 0; i < eventList.Count; i++)
            {

                Console.WriteLine($"{i}){eventList[i].EventName}");

            }

        }
        public void AddEvent() {
 
            var newEvent = new Event();
            
            JObject newContainerJson = new JObject();

            var optionList = new List<string>() {
                "EventName",
                "EventText",
                "InCompany"
            };

            for(int index = 0; index < optionList.Count; index ++)
            {
                Console.WriteLine(optionList[index]);

                newContainerJson.Add(optionList[index], Console.ReadLine());
            }

            newEvent = JsonConvert.DeserializeObject<Event>(newContainerJson.ToString(), INCLUDE_TYPE);

            List<EventType> EventTypes = SetEventType();
            newEvent.EventTypeList = EventTypes;



            eventList.Add(newEvent);


             //convert object into data(string) 
             var data = JsonConvert.SerializeObject(eventList, INCLUDE_TYPE);
            //write file to Event.jason
            FileService.WriteFile("Event.json", data);
        }

        public void AddItem()
        {

            var newItem = new ShopItem();
            Effect Effect = new Effect();
            JObject newContainerJson = new JObject();

            var optionList = new List<string>() {
                "ItemName",
                "ItemDescription",
                "ItemPrice"
            };

            for (int index = 0; index < optionList.Count; index++)
            {
                Console.WriteLine(optionList[index]);

                newContainerJson.Add(optionList[index], Console.ReadLine());
            }

            newItem = JsonConvert.DeserializeObject<ShopItem>(newContainerJson.ToString(), INCLUDE_TYPE);

            itemList.Add(newItem);

            var data = JsonConvert.SerializeObject(itemList, INCLUDE_TYPE);
            FileService.WriteFile("Item.json", data);

        }

        public Option SetOption() {
            var newOption = new Option() { Effects = new List<Effect>() };

            var optionList = new List<string>() {
                "MoneyRequirement",
                "ReputationRequirement",
                "OptionText"
                };

            var newContainerJson = new JObject();
            for (int index = 0; index < optionList.Count; index++)
            {
                Console.WriteLine(optionList[index]);

                newContainerJson.Add(optionList[index], Console.ReadLine());


            }

            newOption = JsonConvert.DeserializeObject<Option>(newContainerJson.ToString(), INCLUDE_TYPE);
            if(newOption.Effects == null)
            {
                newOption.Effects = new List<Effect>();
            }
            return newOption;

        }



        public List<EventType> SetEventType() {
            Console.WriteLine("Setting EventType");
            List<EventType> EventTypes = new List<EventType>();

            Console.WriteLine("EventType(123): morningEvent(1), noonEvent(2), nightEvent(3)");
            string container = Console.ReadLine();
            char[] list = container.ToCharArray();
            for (int i = 0; i < list.Length; i++)
            {
                switch (list[i])
                {
                    case '1':
                        EventTypes.Add(EventType.morningEvent);
                        break;
                    case '2':
                        EventTypes.Add(EventType.noonEvent);
                        break;
                    case '3':
                        EventTypes.Add(EventType.nightEvent);
                        break;
                }
            }

            return EventTypes;
        }

        public CalculationType SetCalculationType() {
            Console.WriteLine("Setting calculationType");
            Console.WriteLine("calculationType(1234): addtion(1), multipulication(2), random(3),abilityChange(4)");
            int container = InputService.TakeInt();

            CalculationType CalculationType = new CalculationType();

            switch (container)
            {
                case 1:
                    CalculationType = CalculationType.addtion;
                    break;
                case 2:
                    CalculationType = CalculationType.multipulication;
                    break;
                case 3:
                    CalculationType = CalculationType.random;
                    break;
                case 4:
                    CalculationType = CalculationType.abilityChange;
                    break;
            }

            return CalculationType;

        }

        public StaffChange SetStaffChange() {
            Console.WriteLine("Setting Staff Change");

            StaffChange StaffChange = new StaffChange();

            var optionList = new List<string>() {
                    "EffectStaffAmount",
                    "AdditionAbility",
             };

            var newContainerJson = new JObject();
            for (int index = 0; index < optionList.Count; index++)
            {
                Console.WriteLine(optionList[index]);

                newContainerJson.Add(optionList[index], Console.ReadLine());
            }

            StaffChange = JsonConvert.DeserializeObject<StaffChange>(newContainerJson.ToString(), INCLUDE_TYPE);

            StaffChange.calculationType = SetCalculationType();

            return StaffChange;
        }

        public MoneyChange SetMoneyChange() {
            Console.WriteLine("Setting Money Change");
            MoneyChange MoneyChange = new MoneyChange();
            var optionList = new List<string>() {
                    "Value",
             };
            var newContainerJson = new JObject();
            for (int index = 0; index < optionList.Count; index++)
            {
                Console.WriteLine(optionList[index]);

                newContainerJson.Add(optionList[index], Console.ReadLine());
            }
            MoneyChange = JsonConvert.DeserializeObject<MoneyChange>(newContainerJson.ToString(), INCLUDE_TYPE);

            MoneyChange.calculationType = SetCalculationType();
            return MoneyChange;
        }

        public ReputationChange SetReputationChange() {
            Console.WriteLine("Setting Reputation Change");
            ReputationChange ReputationChange = new ReputationChange();
            var optionList = new List<string>() {
                    "Value",
             };
            var newContainerJson = new JObject();
            for (int index = 0; index < optionList.Count; index++)
            {
                Console.WriteLine(optionList[index]);

                newContainerJson.Add(optionList[index], Console.ReadLine());
            }
            ReputationChange = JsonConvert.DeserializeObject<ReputationChange>(newContainerJson.ToString(), INCLUDE_TYPE);

            ReputationChange.calculationType = SetCalculationType();
            return ReputationChange;

        }

        public Effect EffectCreator(bool isItem=false) {
            Console.WriteLine("what type of effect do you like?");
            Console.WriteLine("1)Instant Effect");
            Console.WriteLine("2)Instant Duration Effect");
            Console.WriteLine("3)End Day Duration Effect");
            int ans = InputService.TakeInt();
            var newContainerJson = new JObject();
            var optionList = new List<string>();

            Effect resultEffect;

            if (isItem == false)
            {

                optionList = new List<string>() {

                        "OptionResult",
                        "Weight",

                 };
            }


            Console.WriteLine("do you have staff change?");
            Console.WriteLine("1)yes");
            Console.WriteLine("2)no");
            int staffAns = InputService.TakeInt();

            if (ans == 1)
            {
                resultEffect = new InstantEffect();


            }
            else if (ans == 2) {
                resultEffect = new InstantDurationEffect();
                
                optionList.Add("IsDuration");
                optionList.Add("Duration");

            } else {
                resultEffect = new EndDurationEffect();

                optionList.Add("IsDuration");
                optionList.Add("Duration");
                optionList.Add("IsEndEffect");



            }


            newContainerJson = new JObject();
            for (int index = 0; index < optionList.Count; index++)
            {
                Console.WriteLine(optionList[index]);

                newContainerJson.Add(optionList[index], Console.ReadLine());
            }


            if (ans == 1)
            {

                resultEffect = JsonConvert.DeserializeObject<InstantEffect>(newContainerJson.ToString(), INCLUDE_TYPE);

            }
            else if (ans == 2)
            {

                resultEffect = JsonConvert.DeserializeObject<InstantDurationEffect>(newContainerJson.ToString(), INCLUDE_TYPE);

            }
            else
            {

                resultEffect = JsonConvert.DeserializeObject<EndDurationEffect>(newContainerJson.ToString(), INCLUDE_TYPE);

            }

            if (staffAns == 1)
            {
                resultEffect.StaffChange = new StaffChange();
                resultEffect.StaffChange = SetStaffChange();
            }
            resultEffect.MoneyChange = SetMoneyChange();
            resultEffect.ReputationChange = SetReputationChange();


            return resultEffect;
        }
    }
}
