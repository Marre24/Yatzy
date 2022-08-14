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
        private readonly string protokollFileName = "YatzyProtokoll.jpg";
        private readonly string kolumnFileName = "YatzyKolumn.jpg";
        private readonly List<PictureBox> DieBoxes = new List<PictureBox>();
        private readonly List<string> diePics = new List<string>() { "1.jpg", "2.jpg", "3.jpg", "4.jpg", "5.jpg", "6.jpg" };
        private readonly List<CheckBox> checkList = new List<CheckBox>();
        private Table tempTable;

        public Canvas()
        {

        }
        public void CanvasSetUp(Form form, Table table)
        {
            tempTable = table;

            foreach (Player player in tempTable.SortedPlayerList)
            {
                SetRowsFor(player, form);
            }
            PaintCanvas(form);
            DieSetup(form);
        }

        public void PaintCanvas(Form form)
        {
            Point point = new Point(0, 0);
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(200, 1000),
                Location = point,
                Image = Image.FromFile(protokollFileName),
                SizeMode = PictureBoxSizeMode.StretchImage

            };
            pictureBox.Show();
            form.Controls.Add(pictureBox);
            int x = pictureBox.Size.Width;
            for (int i = 1; i < tempTable.SortedPlayerList.Count + 1; i++)
            {
                point.X = x * i;
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
            Point p = new Point(207 + 207 * (player.playerId - 1), 95);
            for (int i = 0; i < 20; i++)
            {
                if (i == 6)
                {
                    p.Y += 84;
                }

                TextBox tb = new TextBox()
                {
                    Size = new Size(190, 100),
                    Location = p,
                    Text = "0",
                    Font = new Font(new FontFamily("Arial"), 21, FontStyle.Regular, GraphicsUnit.Pixel),
                    Enabled = false,
                    Visible = true,
                    TabStop = false,
                };

                tb.Click += new System.EventHandler(this.TextBoxOnClick);
                form.Controls.Add(tb);
                player.points.Add(tb);

                p.Y += 39;
            }

            TextBox tbx = new TextBox()
            {
                Size = new Size(100, 100),
                Location = new Point(p.X, 8),
                Font = new Font(new FontFamily("Arial"), 22, FontStyle.Bold, GraphicsUnit.Pixel),
                Name = "PlayerName",
                Text = player.name,
                ReadOnly = false,
                ForeColor = Color.Red,
                TabStop = false,
            };
            form.Controls.Add(tbx);
            player.mscTextBoxes.Add(tbx);

            for (int j = 1; j < 3; j++)
            {
                int y = 332;
                if (j == 2)
                    y = 962;
                TextBox textBox = new TextBox()
                {
                    Size = new Size(190, 100),
                    Location = new Point(p.X, y),
                    Font = new Font(new FontFamily("Arial"), 22, FontStyle.Bold, GraphicsUnit.Pixel),
                    Enabled = false,
                    TabStop = false,
                    Name = $"Sum{j}",
                    Text = "0",
                };
                form.Controls.Add(textBox);
                textBox.BringToFront();
                player.mscTextBoxes.Add(textBox);

                TextBox t = new TextBox()
                {
                    Size = new Size(190, 100),
                    Location = new Point(p.X, 374),
                    Font = new Font(new FontFamily("Arial"), 22, FontStyle.Bold, GraphicsUnit.Pixel),
                    Enabled = false,
                    Name = "Bonus",
                    Text = "0",
                };
                form.Controls.Add(t);
                t.BringToFront();
                player.mscTextBoxes.Add(t);

            }
        }

        public void TextBoxOnClick(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Name = "Confirmed";
            tb.Enabled = false;

            Player activePlayer = tempTable.SortedPlayerList.First();
            activePlayer.EndTurn();
            tempTable.MoveSecondPlayerToFirst(tempTable);

            activePlayer = tempTable.SortedPlayerList.First();
            activePlayer.StartTurn();
        }

        public void DieSetup(Form form)
        {
            Point point = new Point(1770, 0);
            for (int i = 0; i < 6; i++)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Size = new Size(150, 150),
                    Location = point,
                    Image = Image.FromFile(protokollFileName),
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
                    Location = new Point(225 + 216 * tempTable.SortedPlayerList.Count - 1, 500),
                    Text = "Kasta tärningarna",
                    TabStop = false,
                };
                btn.Click += Btn_Click_Event;
                form.Controls.Add(btn);

                Button reset = new Button
                {
                    Size = new Size(100, 100),
                    Location = new Point(225 + 216 * tempTable.SortedPlayerList.Count - 1, 750),
                    Text = "Reset Board",
                    TabStop = false,
                };
                //reset.Click += Btn_Click_Event;
                form.Controls.Add(reset);
            }
        }

        public void Btn_Click_Event(object sender, EventArgs e)
        {
            Player activePlayer = tempTable.SortedPlayerList[0];

            if (activePlayer.remainingThrows == 0)
            {
                MessageBox.Show("No remaining throws");
                return;
            }
            activePlayer.remainingThrows--;


            activePlayer.ThrowDieEvent(activePlayer, checkList);

            int index = 0;
            foreach (int die in activePlayer.savedDice)
            {
                SetPictureForDie(index, die);
                index++;
            }
            ShowPointsOnBoardFor(activePlayer);

        }

        private static void ShowPointsOnBoardFor(Player p)
        {
            Table table = new Table();
            List<int> points = table.GetAllValuesFor(p.savedDice);

            for (int i = 0; i < points.Count; i++)
            {
                if (p.points[i].Enabled)
                {
                    p.points[i].Text = points[i].ToString();
                }
            }
        }

        public void SetPictureForDie(int index, int dienum)
        {
            DieBoxes[index].Image = Image.FromFile(diePics[dienum - 1]);
        }


    }
}
