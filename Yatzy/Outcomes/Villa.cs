using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    public class Villa : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;
            int trip  = 0;
            int trip2 = 0;

            List<int> tempList = dice.ToList();

            for (int i = 0; i < dice.Count; i++)
            {

                int temp = dice[i];


                if (ContainsXInList(temp, tempList, 3))
                {
                    trip = temp * 3;
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


                if (ContainsXInList(temp, tempList, 3))
                {
                    trip2 = temp * 3;
                    break;
                }
            }

            if (!(trip == 0 || trip2 == 0))
            {
                value = trip2 + trip;
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
