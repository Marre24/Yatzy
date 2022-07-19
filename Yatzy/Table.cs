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
        public List<Player> SortedPlayerList { get { return sortedPlayerList; } set { sortedPlayerList = SortedPlayerList; }}

        public Table()
        {
            AddFuncsToList();
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

        readonly List<Func<List<int>, int>> getValueFuncs = new List<Func<List<int>, int>>();

        readonly Ones ones = new Ones();
        readonly Twos twos = new Twos();
        readonly Threes threes = new Threes();
        readonly Fours fours = new Fours();
        readonly Fives fives = new Fives();
        readonly Sixes sixes = new Sixes();

        private void AddFuncsToList()
        {
            //GetValue funcitions
            Func<List<int>, int> onesOutcomeFunc = ones.GetValue;
            Func<List<int>, int> twosOutcomeFunc = twos.GetValue;
            Func<List<int>, int> threesOutcomeFunc = threes.GetValue;
            Func<List<int>, int> foursOutcomeFunc = fours.GetValue;
            Func<List<int>, int> fivesOutcomeFunc = fives.GetValue;
            Func<List<int>, int> sixesOutcomeFunc = sixes.GetValue;


            getValueFuncs.Add(onesOutcomeFunc);
            getValueFuncs.Add(twosOutcomeFunc);
            getValueFuncs.Add(threesOutcomeFunc);
            getValueFuncs.Add(foursOutcomeFunc);
            getValueFuncs.Add(fivesOutcomeFunc);
            getValueFuncs.Add(sixesOutcomeFunc);
        }

        public List<int> GetAllValuesFor(List<int> dice)
        {
            List<int> pointsForTheseDice = new List<int>();
            foreach (Func<List<int>, int> func in getValueFuncs)
            {
                pointsForTheseDice.Add(func(dice));
            }
            return pointsForTheseDice;
        }

    }
}
