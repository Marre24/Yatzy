using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    internal class Fours : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;
            foreach (int item in dice)
            {
                if (item == 4)
                {
                    value += 4;
                }
            }

            return value;
        }
    }
}
