using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    internal class Ones : PossibleOutcome
    {
        public override bool AchievesRequirement(List<int> dice)
        {
            int counter = 0;
            foreach (int item in dice)
            {
                if (item == 1)
                {
                    counter++;
                }
            }
            
            return counter > 0;
        }

        public override int GetValue(List<int> dice)
        {
            int counter = 0;
            foreach (int item in dice)
            {
                if (item == 1)
                {
                    counter += 1;
                }
            }

            return counter;
        }


    }
}
