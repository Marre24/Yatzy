using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    public class PossibleOutcome
    {
        public virtual bool AchievesRequirement(List<int> dice)
        {


            return false;
        }

        public virtual int GetValue(List<int> dice)
        {

            return 0;
        }

    }
}
