using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.Service
{
    internal class FileService
    {
        /// <summary>
        /// read file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ReadFile(string fileName, bool isSaveFile = false)
        {   
            //long string type
            //set a varible
            StringBuilder data = new StringBuilder();

            string location;
            if (isSaveFile)
            {
                location = Path.Combine(Environment.CurrentDirectory, @"SaveFile\", fileName);


            }
            else
            {
                location = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);


            }

            //use StreamReader to read content from the file
            //Environment.CurrentDirectory(where the this file.exe at in computer)\\@"Data\\fileName
            //                                          Combine the path of the file 
            using (StreamReader file = new StreamReader(Path.Combine(location)))
            {
                string? ln;
                //read each line and add it to ln until reach the end of the file
                while ((ln = file.ReadLine()) != null)
                {
                    //add the line of file content into data
                    data.Append(ln);
                }
                //close the file(release construe)
                file.Close();
            }
            //change StringBuilder to string
            return data.ToString();
        }
        /// <summary>
        /// write data in to the file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        public static void WriteFile(string fileName, string data, bool isSaveFile = false)
        {
            string location;
            if (isSaveFile) {
                location = Path.Combine(Environment.CurrentDirectory, @"SaveFile\", fileName);


            }
            else {
                location = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);


            }
            //C:user/document/desktop\Data.txt
            //using(makesure the thing in brackets will be close at the end)
            //set StreamWriter to write string into file in         this path ↓
            using (StreamWriter outputFile = new StreamWriter(location))
            {
                //write contents in data to the file
                outputFile.WriteLine(data);
                outputFile.Close();
            }


        }




       /* public static void EditFile() {
            string json = File.ReadAllText("Event.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            JToken jToken = jsonObj.GetTokens("");

            jsonObj["Bots"][0]["Password"] = "new password";
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("Event.json", output);

        }*/
    }
}
