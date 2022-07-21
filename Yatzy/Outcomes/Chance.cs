using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    internal class Chance : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;

            foreach (int die in dice)
                value += die;
            
            return value;
        }
    }
}
