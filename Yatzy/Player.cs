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

        public TextBox BonusTextBox
        { 
            get 
            {
                foreach (TextBox textBox in mscTextBoxes)
                {
                    if (textBox.Name == "Bonus")
                        return textBox;
                }
                return null;
            }
            set { return; }
        } 

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

        public void SetPoints(List<string> data)
        {
            if (data.Count != points.Count)
                throw new Exception("Ivnalid data length");

            for (int i = 0; i < points.Count;i++)
            {
                points[i].Text = data[i];
                if(data[i] != "0")
                {
                    points[i].Name = "Confirmed";
                    points[i].Enabled = false;
                }
            }
        }

        public void UpdatePointsFromDatabase()
        {
            var response = YatsyForm.firebaseClient.Get("board/" + playerId.ToString());
            var data = response.ResultAs<List<string>>();
            SetPoints(data);
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
            int bonus = 0;
            (int sum1, int sum2) = CalcSums();

            foreach (TextBox textBox in this.mscTextBoxes)
            {

                if (textBox.Name == "Sum1")
                {
                    textBox.Text = sum1.ToString();
                }

                if (textBox.Name == "Bonus" && sum1 >= 84)
                {
                    bonus = 100;
                    textBox.Text = $"{bonus}";
                }

                if (textBox.Name == "Sum2")
                {
                    textBox.Text = (bonus + sum2).ToString();
                }

                

                if (textBox.Name == "PlayerName")
                {
                    textBox.ForeColor = Color.Red;
                }
            }


            foreach (TextBox tb in this.points)
            {
                tb.Enabled = false;
            }
            
            this.savedDice.Clear();

            remainingThrows = 3;
        }

        private (int sum1, int sum2) CalcSums()
        {
            int sum1 = 0;
            int sum2 = 0;

            for (int i = 0; i < 6; i++)
            {
                if (this.points[i].Enabled == false)
                {
                    sum1 += int.Parse(this.points[i].Text);
                }
            }

            int index = 0;
            foreach (TextBox textBox1 in this.points)
            {
                if (textBox1.Enabled == false)
                {
                    sum2 += int.Parse(textBox1.Text);
                }
                else
                {
                    textBox1.Text = "0";
                }

                YatsyForm.firebaseClient.Set("board/" +  playerId +"/" + index.ToString(), textBox1.Text);
                index++;
            }

            return (sum1, sum2);
        }

        public void StartTurn()
        {
            Player p = this;
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
