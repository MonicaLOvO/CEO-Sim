using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation.EffectFolder
{
    /// <summary>
    /// child of Effect class
    /// </summary>
    internal class DurationEffect : Effect
    {
        //create attribute
        public int Duration { get; set; }
        public bool IsEndEffect { get; set; }

    }

}
