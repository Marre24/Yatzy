using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    public class LargeStraight : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;
            SortedSet<int> sortedSet = new SortedSet<int>(dice.ToList());

            if (sortedSet.Min == 1)
                sortedSet.Remove(1);

            if (sortedSet.Max == 6 && sortedSet.Count == 5)
            {
                value = 2 + 3 + 4 + 5 + 6;
            }
            return value;
        }
    }
}
