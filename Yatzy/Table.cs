﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yatzy.Outcomes;

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
            canvas.CanvasSetUp(form, table);
        }

        internal void Join(Player player)
        {
            sortedPlayerList.Add(player);
        }

        public void MoveSecondPlayerToFirst(Table t)
        {
            Player player = sortedPlayerList.First();
            t.SortedPlayerList.RemoveAt(0);
            t.SortedPlayerList.Insert(t.SortedPlayerList.Count, player);
        }

        readonly List<Func<List<int>, int>> getValueFuncs = new List<Func<List<int>, int>>();

        readonly Ones ones = new Ones();
        readonly Twos twos = new Twos();
        readonly Threes threes = new Threes();
        readonly Fours fours = new Fours();
        readonly Fives fives = new Fives();
        readonly Sixes sixes = new Sixes();

        readonly Pair pair = new Pair();
        readonly TwoPairs twoPairs = new TwoPairs();
        readonly ThreePairs threePairs = new ThreePairs();
        readonly Trips trips = new Trips();
        readonly FourOfAKind fourOfAKind = new FourOfAKind();
        readonly FiveOfAKind fiveOfAKind = new FiveOfAKind();
        readonly SmallStraight smallStraight = new SmallStraight();
        readonly LargeStraight largeStright = new LargeStraight();
        readonly FullStraight fullStright = new FullStraight();
        readonly FullHouse fullHouse = new FullHouse();
        readonly Villa villa = new Villa();
        readonly Tower tower = new Tower();
        readonly Chance chance = new Chance();
        readonly Maxi_Yatzy maxi_Yatzy = new Maxi_Yatzy();


        private void AddFuncsToList()
        {
            //GetValue funcitions

            getValueFuncs.Add(ones.GetValue);
            getValueFuncs.Add(twos.GetValue);
            getValueFuncs.Add(threes.GetValue);
            getValueFuncs.Add(fours.GetValue);
            getValueFuncs.Add(fives.GetValue);
            getValueFuncs.Add(sixes.GetValue);
            getValueFuncs.Add(pair.GetValue);
            getValueFuncs.Add(twoPairs.GetValue);
            getValueFuncs.Add(threePairs.GetValue);
            getValueFuncs.Add(trips.GetValue);
            getValueFuncs.Add(fourOfAKind.GetValue);
            getValueFuncs.Add(fiveOfAKind.GetValue);
            getValueFuncs.Add(smallStraight.GetValue);
            getValueFuncs.Add(largeStright.GetValue);
            getValueFuncs.Add(fullStright.GetValue);
            getValueFuncs.Add(fullHouse.GetValue);
            getValueFuncs.Add(villa.GetValue);
            getValueFuncs.Add(tower.GetValue);
            getValueFuncs.Add(chance.GetValue);
            getValueFuncs.Add(maxi_Yatzy.GetValue);
        }

        internal void StartGame(List<Button> buttonList, Button throwDiceBtn)
        {
            if (SortedPlayerList.Count <= 0)
            {
                MessageBox.Show("You can not start the game with 0 players");
                return;
            }
            foreach (Button button in buttonList)
            {
                button.Hide();
            }
            throwDiceBtn.Show();
            SortedPlayerList.First().StartTurn();
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

        internal bool IsGameOver()
        {
            foreach (Player player in sortedPlayerList)
            {
                foreach (var item in player.points)
                {
                    if (item.Name != "Confirmed")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        internal void RemovePlayerFromList(Player player)
        {
            sortedPlayerList.Remove(player);
        }
    }
}
