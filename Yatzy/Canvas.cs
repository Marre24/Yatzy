using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Net.Http.Headers;

namespace Yatzy
{
    public class Canvas
    {
        private readonly string outcomeFileName = "YatzyProtokoll.jpg";
        private readonly string kolumnFileName = "YatzyKolumn.jpg";
        public readonly List<PictureBox> DieBoxes = new List<PictureBox>();
        private readonly List<string> diePics = new List<string>() { "1.jpg", "2.jpg", "3.jpg", "4.jpg", "5.jpg", "6.jpg" };
        public readonly List<CheckBox> checkList = new List<CheckBox>();
        private List<Player> sortedPlayerList;

        public Canvas()
        {

        }
        public void CanvasSetUp(Form form, List<Player> players, Table table)
        {

            sortedPlayerList = table.SortedPlayerList;
            foreach (Player player in players)
            {
                SetRowsFor(player, form);
            }
            PaintCanvas(form, players);
            DieSetup(form, players);
        }

        public void PaintCanvas(Form form, List<Player> players)
        {
            Point point = new Point(0, 0);
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(216, 1000),
                Location = point,
                Image = Image.FromFile(outcomeFileName),
                SizeMode = PictureBoxSizeMode.StretchImage

            };
            pictureBox.Show();
            form.Controls.Add(pictureBox);
            int x = pictureBox.Size.Width;
            for (int i = 0; i < players.Count; i++)
            {
                point.X += x;
                PictureBox pictureBox1 = new PictureBox
                {
                    Size = new Size(216, 1000),
                    Location = point,
                    Image = Image.FromFile(kolumnFileName),
                    SizeMode = PictureBoxSizeMode.StretchImage

                };
                pictureBox1.Show();
                form.Controls.Add(pictureBox1);

                for (int j = 0; j < 13; j++)
                {

                    TextBox textBox = new TextBox
                    {
                        Size = new Size(216, 1000),
                        Location = point
                    };
                    point.X = textBox.Width;
                }
            }
        }
        public void SetRowsFor(Player player, Form form)
        {
            Point p = new Point(218 * player.playerId, 90);
            for (int i = 0; i < 20; i++)
            {
                if (i == 6)
                {
                    p.Y += 87;
                }

                TextBox tb = new TextBox()
                {
                    Size = new Size(200, 100),
                    Location = p,
                    Text = "Testing heheheh",
                    Font = new Font(new FontFamily("Arial"), 20, FontStyle.Regular, GraphicsUnit.Pixel),
                    ReadOnly = true,
                    Visible = true,
                    TabStop = false
                };

                tb.Click += new System.EventHandler(this.SetPoints);
                form.Controls.Add(tb);
                player.rows.Add(tb);

                p.Y += 39;
            }
        }

        public void SetPoints(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Enabled = false;
        }

        public void DieSetup(Form form, List<Player> ps)
        {
            Point point = new Point(1770, 0);
            for (int i = 0; i < 6; i++)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Size = new Size(150, 150),
                    Location = point,
                    Image = Image.FromFile(outcomeFileName),
                    SizeMode = PictureBoxSizeMode.StretchImage

                };
                pictureBox.Show();
                form.Controls.Add(pictureBox);
                DieBoxes.Add(pictureBox);
                CheckBox cb = new CheckBox
                {
                    Size = new Size(100, 100),
                    Location = new Point(point.X - 175, point.Y),
                    TabIndex = i,
                    Visible = false,
                };
                form.Controls.Add(cb);
                checkList.Add(cb);
                point.Y += 160;

                Button btn = new Button
                {
                    Size = new Size(100, 100),
                    Location = new Point(225 + 216 * ps.Count - 1, 500),
                    Text = "Kasta tärningarna"
                };
                btn.Click += ThrowDieEvent;
                form.Controls.Add(btn);
            }
        }

        public void ThrowDieEvent(object sender, EventArgs e)
        {
            Player activePlayer = sortedPlayerList[0];
            int amountOfDiceToThrow = 0;
            

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

            new Player().ThrowDiesFor(activePlayer, amountOfDiceToThrow);
            for (int index = 0; 6 > index; index++)
            {
                SetPictureForDie(index, activePlayer.savedDice[index]);
            }

            ShowPointsOnBoardFor(activePlayer);
        }

        private void ShowPointsOnBoardFor(Player p)
        {
            Table table = new Table();
            List<int> points = table.GetAllValuesFor(p.savedDice);

            for (int i = 0; i < points.Count; i++)
            {
                if (p.rows[i].Enabled)
                {
                    p.rows[i].Text = points[i].ToString();
                }
            }
        }

        public void SetPictureForDie(int index, int dienum)
        {
            DieBoxes[index].Image = Image.FromFile(diePics[dienum - 1]);
        }


    }
}
