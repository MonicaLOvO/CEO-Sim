using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CEO_simulator.EntityInfomation.EffectFolder.ActualEffects
{
    internal class MoneyChange
    {
        public double Value { get; set; }
        public CalculationType calculationType { get; set; }
    }
}
