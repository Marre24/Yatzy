using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yatzy
{
    public class Table
    {

        private List<Player> sortedPlayerList = new List<Player>();
        readonly Canvas canvas = new Canvas();

        public List<Player> SortedPlayerList {get; set;}

        public Table()
        {

        }

        public void SetTableIn(Form form, Table table)
        {
            canvas.CanvasSetUp(form, sortedPlayerList, table);
        }

        internal void Join(Player player)
        {
            sortedPlayerList.Add(player);
        }

        private void MoveSecondPlayerToFirst()
        {
            var player = sortedPlayerList[1];
            sortedPlayerList.RemoveAt(1);
            sortedPlayerList.Insert(0, player);
        }
    }
}
