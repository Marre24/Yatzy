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
        public List<TextBox> points = new List<TextBox>(); //20 st
        public List<TextBox> mscTextBoxes = new List<TextBox>();
        public List<int> savedDice = new List<int>();
        public int playerId = 1;
        private static int _nextId = 1;
        public string name = $"Bert{_nextId}";

        public int remainingThrows = 3;

        public Player()
        {
            playerId = _nextId++;
        }

        public void ThrowDiesFor(Player player, int dieCount)
        {
            Random r = new Random();
            for (int i = 0; i < dieCount; i++)
            {
                int die = r.Next(1, 7);
                player.savedDice.Add(die);
            }
            player.savedDice.Sort();
        }

        public void ThrowDieEvent(Player activePlayer, List<CheckBox> checkList)
        {
            int amountOfDiceToThrow = 0;

            foreach (TextBox textBox in activePlayer.points)
            {
                if (!(textBox.Name == "Confirmed"))
                    textBox.Enabled = true;
            }


            if (activePlayer.savedDice.Count < 6)
            {
                foreach (CheckBox checkBox in checkList)
                {
                    checkBox.Visible = true;
                }
                amountOfDiceToThrow = 6;
            }
            else
            {
                int counter = 5;
                foreach (int die in activePlayer.savedDice.ToList())
                {

                    if (checkList[counter].Checked)
                    {
                        activePlayer.savedDice.RemoveAt(counter);
                        amountOfDiceToThrow++;
                    }
                    counter--;
                }
            }

            if (amountOfDiceToThrow == 0)
            {
                MessageBox.Show("You can not throw zero die");
                return;
            }

            activePlayer.ThrowDiesFor(activePlayer, amountOfDiceToThrow);

        }

        public void EndTurn()
        {
            foreach (TextBox textBox in this.mscTextBoxes)
            {
                int temp = 0;

                if (textBox.Name == "Sum1")
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (this.points[i].Enabled == false)
                        {
                            temp += int.Parse(this.points[i].Text);
                        }
                    }
                    textBox.Text = temp.ToString();
                }

                temp = 0;

                if (textBox.Name == "Sum2")
                {
                    foreach (TextBox textBox1 in this.points)
                    {
                        if (textBox1.Enabled == false)
                        {
                            temp += int.Parse(textBox1.Text);
                        }
                        else
                        {
                            textBox1.Text = "0";
                        }
                        textBox.Text = temp.ToString();
                    }
                }

                if (textBox.Name == "PlayerName")
                {
                    textBox.ForeColor = Color.Red;
                }


            }

            this.savedDice.Clear();

            foreach (TextBox tb in this.points)
            {
                tb.Enabled = false;
            }

            remainingThrows = 3;
        }

        public void StartTurnFor(Player p)
        {
            remainingThrows = 3;
            foreach (TextBox textBox1 in p.mscTextBoxes)
            {
                if (textBox1.Name == "PlayerName")
                {
                    textBox1.ForeColor = Color.Green;
                }
            }

            foreach (TextBox textBox in p.points)
            {
                if (!(textBox.Name == "Confirmed"))
                    textBox.Enabled = true;
            }


        }
    }
}
