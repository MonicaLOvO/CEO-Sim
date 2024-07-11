using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation
{
    internal class Event
    {
        //create attribute
        public List<EventType> EventTypeList { get; set; }
        public string EventText{ get; set; }
        public string EventName { get; set; }
        public bool InCompany { get; set; }
        
        public List<Option> OptionList;



        //constructor
        public Event() {
            OptionList = new List<Option>();
            EventTypeList = new List<EventType>();
        }

    }
    public enum EventType
    {
        morningEvent= 1,
        noonEvent = 2,
        nightEvent = 3,

    }
}
