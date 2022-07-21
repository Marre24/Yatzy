using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    public class FullStraight : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;
            SortedSet<int> sortedSet = new SortedSet<int>(dice.ToList());


            if (sortedSet.Count == 6)
            {
                value = 1 + 2 + 3 + 4 + 5 + 6;
            }
            return value;
        }
    }
}
