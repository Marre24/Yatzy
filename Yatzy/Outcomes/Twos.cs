using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    internal class Twos : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;
            foreach (int item in dice)
            {
                if (item == 2)
                {
                    value += 2;
                }
            }

            return value;
        }
    }
}
