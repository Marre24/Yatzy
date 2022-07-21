using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    internal class Maxi_Yatzy : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;

            SortedSet<int> ts = new SortedSet<int>(dice.ToList());

            if (ts.Count == 1)
                value = 100;

            return value;
        }
    }
}
