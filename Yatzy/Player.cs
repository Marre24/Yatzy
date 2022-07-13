using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yatzy
{
    public class Player
    {
        public List<TextBox> rows = new List<TextBox>(); //20 st
        public List<int> savedDice = new List<int>();
        public List<int> dicardedDice = new List<int>();
        public State state = new State(StateType.Waiting);
        public int playerId = 1;
        private static int _nextId = 1;

        public Player()
        {
            playerId = _nextId++;
        }

        public void ThrowDiesFor(int dieCount, Player player)
        {
            List<int> dice = new List<int>();
            Random r = new Random();
            for (int i = 0; i < dieCount; i++)
            {
                int die = r.Next(1,7);
                dice.Add(die);
            }

            player.savedDice = dice;
        }
    }
}
