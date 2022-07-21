using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    internal class TwoPairs : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;

            for (int i = 0; i < dice.Count; i++)
            {
                List<int> TempList = dice.ToList();
                TempList.RemoveAt(i);
                int tempNum = dice[i];

                for (int j = 0; j < TempList.Count; j++)
                {
                    if (tempNum == TempList[j])
                    {
                        TempList.RemoveAt(j);

                        for (int o = 0; o < TempList.Count; o++)
                        {
                            int temp = TempList[o];
                            TempList.RemoveAt(o);
                            if (TempList.Contains(temp))
                            {

                                value = temp*2 + tempNum*2;
                            }
                        }
                    }
                }
            }
            return value;
        }

        
    }
}
