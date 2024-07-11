using CEO_simulator.EntityInfomation;
using CEO_simulator.EntityInfomation.EffectFolder;
using CEO_simulator.MainLogic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CEO_simulator.Service
{
    internal class SaveFileService
    {
        //the type wanted when convert Json
        private static JsonSerializerSettings INCLUDE_TYPE = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        private static string location = Path.Combine(Environment.CurrentDirectory, @"SaveFile");

        
        /// <summary>
        /// method to get file name
        /// </summary>
        /// <param name="isAuto"></param>
        public static void SaveNewJson(bool isAuto=true)
        {
            //if is auto save
            if (isAuto) 
            {
                WirteSaveFile("Auto_Save");
            } 
            else 
            {
                string[] fileLocation = Directory.GetFiles(location);
                Console.WriteLine("do you want to save in a newfile? or replace an old one?(maxmum 10)");
                int chose;
                // if save file is exist and less than 11
                if (fileLocation.Length < 11 && fileLocation.Length >= 1)
                {
                    Console.WriteLine("1) newfile");
                    Console.WriteLine("2) replace");
                    chose = InputService.TakeInt(1, 2);

                }
                // if save file is more than 10
                else if (fileLocation.Length >= 1)
                {
                    Console.WriteLine("2) replace");
                    chose = InputService.TakeInt(2, 2);

                }
                //if there is no save file 
                else {

                    Console.WriteLine("1) newfile");
                    chose = InputService.TakeInt(1, 1);

                }


                string? fileName = null;
                //add new file
                if (chose == 1)
                {
                    while (string.IsNullOrEmpty(fileName) == true)
                    {
                        Console.WriteLine("Pleace enter the savefile name:");
                        fileName = Console.ReadLine();
                        if (string.IsNullOrEmpty(fileName))
                        {
                            Console.WriteLine("The file name is empty");
                        }
                        else
                        {
                            break;
                        }

                    }
                } 
                //replace old file
                else {
                    ShowFile(fileLocation);

                    Console.WriteLine("\nWhich file do you want to save?");
                    int fileChose = InputService.TakeInt(1, fileLocation.Length + 1);
                    fileName = fileLocation[fileChose - 1];

                }


               WirteSaveFile(fileName);
            }

    }
        /// <summary>
        /// method to write a save file with file name
        /// </summary>
        /// <param name="fileName"></param>
        public static void WirteSaveFile(string fileName) {

            JObject data = new JObject();

            data.Add("player", JsonConvert.SerializeObject(GameLogic.player, INCLUDE_TYPE));
            data.Add("staffList", JsonConvert.SerializeObject(GameLogic.comp.StaffList, INCLUDE_TYPE));
            data.Add("level", GameLogic.comp.Level);
            data.Add("durationList", JsonConvert.SerializeObject(GameLogic.effectLogic.durationList, INCLUDE_TYPE));


            string json = JsonConvert.SerializeObject(data, INCLUDE_TYPE);
            FileService.WriteFile(fileName, json, true);

        }

        /// <summary>
        /// load the file
        /// </summary>
        public static void LoadSaveFile()
        {
            string location = Path.Combine(Environment.CurrentDirectory, @"SaveFile");
            string[] fileLocation = Directory.GetFiles(location);
            for(int i = 0; i < fileLocation.Length; i++)
            {
                Console.WriteLine($"{(i+1)}) {Path.GetFileName(fileLocation[i])}");
            }

            Console.WriteLine("\nWhich data do you want to load?");
            int chose  = InputService.TakeInt(1, fileLocation.Length+1);
            ReadSaveFile(fileLocation[chose - 1]);
        }
        /// <summary>
        /// read the data in the file
        /// </summary>
        /// <param name="fileName"></param>
        public static void ReadSaveFile(string fileName)
        {
            JObject data = JsonConvert.DeserializeObject<JObject>(FileService.ReadFile(fileName, true), INCLUDE_TYPE);

            GameLogic.player = JsonConvert.DeserializeObject<Player>(data["player"].ToString(), INCLUDE_TYPE);
            GameLogic.comp.StaffList = JsonConvert.DeserializeObject<List<Staff>>(data["staffList"].ToString(), INCLUDE_TYPE);
            GameLogic.comp.Level = JsonConvert.DeserializeObject<int>(data["level"].ToString(), INCLUDE_TYPE);
            GameLogic.effectLogic.durationList = JsonConvert.DeserializeObject<List<DurationEffect>>(data["durationList"].ToString(), INCLUDE_TYPE);
        }

        /// <summary>
        /// use the data to set up the game
        /// </summary>
        /// <returns></returns>
        public static bool LoadGame()
        {

            string[] fileLocation = Directory.GetFiles(location);
            bool result;
            if (Directory.Exists(location) == false)
            {
                Directory.CreateDirectory(location);
            }

            if (fileLocation.Length < 1)
            {

                return false;

            }
            else {
                CleanScreen.Clean();

                Console.WriteLine("Welcome, Player! do you like to start a new game? or continue from where you saved?");
                Console.WriteLine("1) Start a new game");
                Console.WriteLine("2) Load save file");
                Console.WriteLine("3) Delete save file");
                int chose = InputService.TakeInt(1, 3);


                if (chose == 2)
                {
                    SaveFileService.LoadSaveFile();
                    return true;
                }
                else if (chose == 3)
                {

                    return DeleteFile();
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// (THE BEST RECURSIVE!!!) delete save file
        /// </summary>
        /// <returns></returns>
        public static bool DeleteFile()
        {
            CleanScreen.Clean();
            int chose=1;
            string[] fileLocation;

            while (chose!=0)
            {
                fileLocation = Directory.GetFiles(location);
                if (fileLocation.Length <1)
                {
                    Console.WriteLine("\nYou have no save file left");
                    CleanScreen.Clean(2);
                    break;
                }

                CleanScreen.Clean();
                Console.WriteLine("Which file do you want to delete?");
                Console.WriteLine("0) Exit");

                ShowFile(fileLocation);

                int fileChose = InputService.TakeInt(0, fileLocation.Length);
                if (fileChose == 0)
                {
                    break;
                }
                else {
                    File.Delete(fileLocation[fileChose - 1]);

                }
                
            }

            return LoadGame();
        }

        /// <summary>
        /// print file that exist
        /// </summary>
        /// <param name="fileLocation"></param>
        public static void ShowFile(string[] fileLocation) {
            for (int i = 0; i < fileLocation.Length; i++)
            {
                Console.WriteLine($"{(i + 1)}) {Path.GetFileName(fileLocation[i])}");
            }
        }
    }
}
