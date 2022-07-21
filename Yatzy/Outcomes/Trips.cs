using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Outcomes
{
    public class Trips : PossibleOutcome
    {
        public override int GetValue(List<int> dice)
        {
            int value = 0;

            for (int i = 0; i < dice.Count; i++)
            {
                List<int> tempList = dice.ToList();
                int temp = tempList[i];

                tempList.Remove(temp);
                if (tempList.Contains(temp))
                {
                    tempList.Remove(temp);
                    if (tempList.Contains(temp))
                    {
                        tempList.Remove(temp);
                        value = temp*3;
                    }
                }

            }

            return value;
        }
    }
}
