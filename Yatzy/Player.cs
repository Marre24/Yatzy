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
        public int playerId = 1;
        private static int _nextId = 1;

        public Player()
        {
            playerId = _nextId++;
        }

        public void ThrowDiesFor(Player player, int dieCount)
        {
            Random r = new Random();
            for (int i = 0; i < dieCount; i++)
            {
                int die = r.Next(1,7);
                player.savedDice.Add(die);
            }
            player.savedDice.Sort();
        }
    }
}
