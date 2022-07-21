using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    internal class Pair : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;

            for (int i = 0; i < dice.Count; i++)
            {
                List<int> TempList = dice.ToList();
                TempList.RemoveAt(i);

                if (TempList.Contains(dice[i]) && value < (dice[i]*2))
                {
                    value = dice[i] * 2;
                }
            }
            return value;
        }
    }
}
