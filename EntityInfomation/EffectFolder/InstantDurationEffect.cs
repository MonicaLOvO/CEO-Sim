using CEO_simulator.EntityInfomation.EffectFolder.ActualEffects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation.EffectFolder
{
    internal class InstantDurationEffect : DurationEffect
    {
        public override JObject ProccessEffect(JObject data, List<DurationEffect>? effectList = null) {

            //get the variable of money and Rep
            double money = data["Money"]?.ToObject<double>() ?? 0;
            double reputation = data["Reputation"]?.ToObject<int>() ?? 0;

            //chack if is money or reputation effect
            if (MoneyChange.calculationType == CalculationType.addtion) {
                money = ProccessAddition(money, true);
            }
            else if (MoneyChange.calculationType == CalculationType.multipulication) {
                money = ProccessMultiplication(money, true);
            }


            if (ReputationChange.calculationType == CalculationType.addtion) {

                reputation = ProccessAddition(reputation, false);

            }
            else if (ReputationChange.calculationType == CalculationType.multipulication)
            {

                reputation = ProccessMultiplication(reputation, false);

            }


            //set data as the result
            data["Money"] = money;
            data["Reputation"] = reputation;

            if (StaffChange != null && StaffChange.EffectStaffAmount != 0)
            {
                ActualStaffEffect.ProccessStaffEffect(StaffChange);

            }


            //return data
            return data;

        }

        /// <summary>
        /// method to run the effect
        /// </summary>
        /// <param name="data"></param>
        /// <param name="effectList"></param>
        /// <returns></returns>
        public double ProccessAddition(double target, bool isMoney)
        {
            //add Effect to the variable

            if (isMoney == true) {

                target += MoneyChange.Value;

            }
            else {

                target += ReputationChange.Value;

            }

            return target;
        }

        public double ProccessMultiplication(double target, bool isMoney)
        {
            //Multiply if MoneyEffect is a positive number
            if (MoneyChange.Value > 0 && isMoney==true)
            {
                target = target * MoneyChange.Value;
            }
            //divide if MoneyEffect is a nigative number
            else if (MoneyChange.Value < 0 && isMoney == true)
            {
                target = target / Math.Abs(MoneyChange.Value);
            }

            //Multiply if ReputationEffect is a positive number
            if (ReputationChange.Value > 0 && isMoney == false)
            {
                target = target * ReputationChange.Value;
            }
            //divide if ReputationEffect is a nigative number
            else if (ReputationChange.Value < 0 && isMoney == false)
            {
                //Abs method to make sure it's positive ↓
                target = target / Math.Abs(ReputationChange.Value);
            }

            //return data
            return target;
        }
    }
}
