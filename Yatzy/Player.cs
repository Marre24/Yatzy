using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    class Player
    {

        public Player()
        {

        }

        public List<int> ThrowDies(int dieCount)
        {
            List<int> dies = new List<int>();
            for (int i = 0; i < dieCount; i++)
            {
                Random r = new Random();
                int die = r.Next(1,6);
                dies.Add(die);
            }

            return dies;
        }

    }
}
