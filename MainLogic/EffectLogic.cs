using CEO_simulator.EntityInfomation;
using CEO_simulator.EntityInfomation.EffectFolder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.MainLogic
{
    internal class EffectLogic
    {
        internal List<DurationEffect> durationList = new List<DurationEffect>();

        /// <summary>
        /// This method should run when an option is selected
        /// </summary>
        /// <param name="targetEffect">Effect about to be proccessed</param>
        public void ProccessOptionEffect(Effect targetEffect)
        {
            //if effect is not exist
            if (targetEffect == null)
            {
                Console.WriteLine("Effect is null");
                return;
            }



            //if the target effect is a duration effect
            if (targetEffect.IsDuration == true)
            {
                // cast targetEffect to child effect
                var durationEffect = JsonConvert.DeserializeObject<DurationEffect>(JsonConvert.SerializeObject(targetEffect));

                durationList.Add(durationEffect);
  
            }
            //if the target effect is a instant effect
            else
            {
                // Get a list of duration effect that could effect Instant Effect
                var notEndEffectList = durationList.Where(e => e.IsEndEffect == false).ToList();
                //create a JSON Object to contain data 
                JObject data = new JObject
                {
                    { "Money", GameLogic.player.TotalMoney },
                    { "Reputation", GameLogic.player.Reputation }
                };

                // Call InstantEffect ProccessEffect method with current player data and protental duration effect
                var instantEffect = JsonConvert.DeserializeObject<InstantEffect>(JsonConvert.SerializeObject(targetEffect));

                //get the result data
                var proccessData = instantEffect?.ProccessEffect(data, notEndEffectList);

                //set player money and Rep as the result data
                GameLogic.player.TotalMoney = proccessData["Money"]?.ToObject<double>() ?? 0;
                GameLogic.player.Reputation = proccessData["Reputation"]?.ToObject<int>() ?? 0;
            
            }
        }

        /// <summary>
        /// This method should run at the end of a game day to proccess all duration effect
        /// </summary>
        public void ProccessEndDayEffect(double turnover)
        {
            //find all the end day Effect in the duration List
            var endDayEffect = durationList.Where(e => e.IsEndEffect == true).ToList();
            Console.WriteLine($"durationList: {durationList.Count}");
            Console.WriteLine($"Number of end day effect: {endDayEffect.Count}");

            //create Json oobject
            var data = new JObject
            {
                //set money as turn over of the day
                { "Money", turnover },
                { "Reputation", GameLogic.player.Reputation }
            };
            //for all the effect in end day effectlist
            foreach (var tempEffect in endDayEffect) {
                
                        var addEffect = JsonConvert.DeserializeObject<EndDurationEffect>(JsonConvert.SerializeObject(tempEffect));
                        data = addEffect?.ProccessEffect(data);
                        break;

                
            }
            
            double moneyAddition=data["Money"]?.ToObject<double>() ?? 0;
            GameLogic.player.TotalMoney += moneyAddition;
            GameLogic.player.Reputation = data["Reputation"]?.ToObject<int>() ?? 0;
            Console.WriteLine($"Money: +{Math.Round(moneyAddition,2)} ");

            for (int i = 0; i < durationList.Count; i++) {
                
                durationList[i].Duration --;
                //check if there is any duration effect end
                if (durationList[i].Duration < 1)
                {
                    durationList.Remove(durationList[i]);
                }
            }
            //loop through the durationList
            foreach (var effect in durationList)
            {
                //print the effect
                Console.WriteLine($"{effect.MoneyChange.Value}$ | {effect.ReputationChange.Value} Rep | Duration: {effect.Duration}");
            }

            //proccess logic
        }
    }
}
