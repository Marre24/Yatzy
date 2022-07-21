using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    public class ThreePairs : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value;


            List<int> TempList = dice.ToList();

            int pairOne = CheckPair(TempList);
            int pairTwo = CheckPair(TempList);
            int pairThree = CheckPair(TempList);


            if (pairOne == 0 || pairThree == 0 || pairTwo == 0)
            {
                return 0;
            }

            value = pairOne + pairTwo + pairThree;
            return value;
        }

        private int CheckPair(List<int> dice)
        {
            
            int value = 0;

            for (int i = 0; i < dice.Count; i++)
            {
                List<int> tempList = dice.ToList();
                int temp = tempList[i];
                tempList.RemoveAt(i);

                if (tempList.Contains(temp) && value == 0)
                {
                    dice.Remove(temp); 
                    dice.Remove(temp);
                    value = temp * 2;
                }
            }

            return value;
        }
    }
}
