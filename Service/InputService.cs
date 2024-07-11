using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.Service
{
    internal class InputService
    {
        /// <summary>
        /// take input of int
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int TakeInt(int? min=null, int? max = null)
        {
            int? result = null;
            if (min == null && max == null) {
                result = Convert.ToInt32(Console.ReadLine());
                return result.Value; 
            } 
            
            else {
                //loop until the result = null 
                while (result == null)
                {
                    try
                    {
                        //take int input and put it in result
                        result = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        //if the input is not a number
                        Console.WriteLine("pleace enter a number");
                        result = null;
                    }

                    if (result < min || result > max)
                    {
                        //if the input is smaller or bigger than the min and max.
                        Console.WriteLine("pleace enter a correct number");
                        result = null;
                    }
                }

                return result.Value;


            }
           
        }
    }
}
