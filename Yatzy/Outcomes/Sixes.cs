using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    internal class Sixes : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;
            foreach (int item in dice)
            {
                if (item == 6)
                {
                    value += 6;
                }
            }

            return value;
        }
    }
}
