using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    public class FiveOfAKind : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;
            for (int i = 0; i < dice.Count; i++)
            {
                List<int> tempList = dice.ToList();
                int temp = dice[i];

                if (ContainsXInList(temp, tempList, 5))
                {
                    value = temp * 5;
                }

            }
            return value;
        }
        private static bool ContainsXInList(int item, List<int> dice, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (dice.Contains(item))
                {
                    dice.Remove(item);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
