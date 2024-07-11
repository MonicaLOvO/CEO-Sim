using CEO_simulator.EntityInfomation.EffectFolder.ActualEffects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation.EffectFolder
{
    /// <summary>
    /// child of Effect class
    /// </summary>
    internal class InstantEffect : Effect
    {
        //create attribute
        public double ActualMoneyEffect {  get; set; }
        public double ActualReputationEffect { get; set; }

        /// <summary>
        /// method to run the effect
        /// </summary>
        /// <param name="data"></param>
        /// <param name="effectList"></param>
        /// <returns></returns>
        public override JObject ProccessEffect(JObject data, List<DurationEffect>? effectList = null)
        {            

            //get the variable of money and Rep
            double playerMoney = data["Money"]?.ToObject<double>() ?? 0;
            double playerReputation = data["Reputation"]?.ToObject<double>() ?? 0;

            //chack if is money or reputation effect
            if (MoneyChange != null && MoneyChange.calculationType == CalculationType.addtion)
            {
                playerMoney = ProccessAdditionEffect(playerMoney, true, effectList);
            }
            else if (MoneyChange!= null && MoneyChange.calculationType == CalculationType.multipulication)
            {
                playerMoney = ProccessMultiplicationEffect(playerMoney, true, effectList);
            }
            if (ReputationChange != null && ReputationChange.calculationType == CalculationType.addtion)
            {

                playerReputation = ProccessAdditionEffect(playerReputation, false, effectList);

            }
            else if (ReputationChange != null && ReputationChange.calculationType == CalculationType.multipulication)
            {

                playerReputation = ProccessMultiplicationEffect(playerReputation, false, effectList);

            }

            data["Money"] = playerMoney;
            data["Reputation"] = playerReputation;
            //check if staff change exist
            if (StaffChange != null && StaffChange.EffectStaffAmount != 0)
            {
                //proccess staff affect
                ActualStaffEffect.ProccessStaffEffect(StaffChange);
            }

            return data;
        }
        /// <summary>
        /// method to run an addition effect
        /// </summary>
        /// <param name="data"></param>
        /// <param name="effectList"></param>
        /// <returns></returns>
        private double ProccessAdditionEffect(double data, bool isMoney, List<DurationEffect>? effectList = null)
        {
            //create JObject of effectData
            var effectData = new JObject();
            if (MoneyChange!=null) {
                effectData.Add("Money", MoneyChange.Value);
            }
            if (ReputationChange != null)
            {
                effectData.Add("Reputation", ReputationChange.Value);
            }
            
            

            // if the durtion effectList is not empty
            if (effectList != null && effectList.Count > 0)
            {
                if (isMoney)
                {
                    //for each durtion effect in the list (multiply first and add after)
                    foreach (var effect in effectList.OrderByDescending(e => e.MoneyChange).ToList())
                    {

                        //cast to child class(AdditionEffect)
                        var addEffect = JsonConvert.DeserializeObject<InstantDurationEffect>(JsonConvert.SerializeObject(effect));
                        //run effect
                        effectData = addEffect?.ProccessEffect(effectData);



                    }

                }
                else {
                    //for each durtion effect in the list (multiply first and add after)
                    foreach (var effect in effectList.OrderByDescending(e => e.ReputationChange).ToList())
                    {
                        //cast to child class(AdditionEffect)
                        var addEffect = JsonConvert.DeserializeObject<InstantDurationEffect>(JsonConvert.SerializeObject(effect));
                        //run effect
                        effectData = addEffect?.ProccessEffect(effectData);
                    }


                }
                
            }

            ActualMoneyEffect = effectData["Money"]?.ToObject<double>() ?? 0;
            ActualReputationEffect = effectData["Reputation"]?.ToObject<int>() ?? 0;

            //add result to the data
            if (isMoney==true) {
                data = data + ActualMoneyEffect;

            } else if (isMoney == false) {
                data = data + ActualReputationEffect;

            }
            
           

            return data;
        }
        /// <summary>
        /// method to run a Multiplication effect
        /// </summary>
        /// <param name="data"></param>
        /// <param name="effectList"></param>
        /// <returns></returns>
        private double ProccessMultiplicationEffect(double data, bool isMoney, List<DurationEffect>? effectList = null)
        {
            //create JObject of effectData
            var effectData = new JObject();
            effectData.Add("Money", MoneyChange.Value);
            effectData.Add("Reputation", ReputationChange.Value);

            // if the durtion effectList is not empty
            if (effectList != null)
            {
                if (isMoney)
                {
                    //for each durtion effect in the list (multiply first and add after)
                    foreach (var effect in effectList.OrderByDescending(e => e.MoneyChange).ToList())
                    {

                        //cast to child class(AdditionEffect)
                        var addEffect = JsonConvert.DeserializeObject<InstantDurationEffect>(JsonConvert.SerializeObject(effect));
                        //run effect
                        effectData = addEffect?.ProccessEffect(effectData);



                    }

                }
                else
                {
                    //for each durtion effect in the list (multiply first and add after)
                    foreach (var effect in effectList.OrderByDescending(e => e.ReputationChange).ToList())
                    {

                        //cast to child class(AdditionEffect)
                        var addEffect = JsonConvert.DeserializeObject<InstantDurationEffect>(JsonConvert.SerializeObject(effect));
                        //run effect
                        effectData = addEffect?.ProccessEffect(effectData);



                    }


                }

            }


            ActualMoneyEffect = effectData["Money"]?.ToObject<double>() ?? 0;
            ActualReputationEffect = effectData["Reputation"]?.ToObject<int>() ?? 0;
            //add result to the data
            if (isMoney == true)
            {
                data = data * ActualMoneyEffect;
            }
            else if (isMoney == false)
            {
                data = data * ActualReputationEffect;
            }
            return data;
        }

    }
}
