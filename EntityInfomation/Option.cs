using CEO_simulator.EntityInfomation.EffectFolder;

namespace CEO_simulator.EntityInfomation
{
    internal class Option
    {
        //create attribute
        public List<Effect> Effects { get; set; }


        public double? MoneyRequirement { get; set; }
        public int? ReputationRequirement { get; set; }
        public string OptionText { get; set; }
        
        public Effect GetRandomEffect()
        {
            Random random = new Random();

            var sum = Effects.Sum(e => e.Weight);
            
            int ranNum = random.Next(1, sum+1);

            int count=0;
            for (int i = 0; i < Effects.Count; i++) {
                int chance = count + Effects[i].Weight;
                if (chance >= ranNum)
                {
                    return Effects[i];

                }
                else {

                    count += Effects[i].Weight;
                }

            }
            

            return Effects.FirstOrDefault();
        }
    }
}
