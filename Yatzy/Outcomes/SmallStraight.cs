using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    public class SmallStraight : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;
            SortedSet<int> sortedSet = new SortedSet<int>(dice.ToList());

            if (sortedSet.Max == 6)
                sortedSet.Remove(6);

            if (sortedSet.Max == 5 && sortedSet.Count == 5)
            {
                value = 1 + 2 + 3 + 4 + 5;
            }

            return value;
        }

    }
}
