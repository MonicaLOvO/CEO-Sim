using CEO_simulator.EntityInfomation.EffectFolder.ActualEffects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation.EffectFolder
{
    internal class Effect
    {
        //create attribute
        public MoneyChange MoneyChange { get; set; }
        public ReputationChange ReputationChange { get; set; }
        public StaffChange StaffChange { get; set; }
        public bool IsDuration { get; set; }
        public int Weight { get; set; }

        public string OptionResult { get; set; }



        //abstract
        public virtual JObject ProccessEffect(JObject data, List<DurationEffect>? effectList = null)
        {
            return data;
        }
    }


}
