using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    public class Tower : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;
            int fourOfAKind = 0;
            int pair = 0;
            List<int> tempList = dice.ToList();

            for (int i = 0; i < dice.Count; i++)
            {

                int temp = dice[i];


                if (ContainsXInList(temp, tempList, 4))
                {
                    fourOfAKind = temp * 4;
                    break;
                }
                else
                {
                    tempList = dice.ToList();
                }
            }

            for (int j = 0; j < tempList.Count; j++)
            {
                int temp = tempList[j];


                if (ContainsXInList(temp, tempList, 2))
                {
                    pair = temp * 2;
                    break;
                }
            }

            if (!(fourOfAKind == 0 || pair == 0))
            {
                value = pair + fourOfAKind;
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
