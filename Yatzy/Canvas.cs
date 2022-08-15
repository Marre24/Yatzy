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
        private readonly string EmptyDieImageFileName = "Empty.jpg";
        private readonly List<PictureBox> DieBoxes = new List<PictureBox>();
        private readonly List<string> diePics = new List<string>() { "1.jpg", "2.jpg", "3.jpg", "4.jpg", "5.jpg", "6.jpg" };
        private readonly List<CheckBox> checkList = new List<CheckBox>();
        private Table tempTable;
        Form tempYatzyForm;

        public Canvas()
        {

        }
        public void CanvasSetUp(Form form, Table table)
        {
            tempTable = table;


            PaintCanvas(form);
            DieSetup(form);
        }

        public void PaintCanvas(Form form)
        {
            tempYatzyForm = form;
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(200, 1000),
                Location = new Point(0, 0),
                Image = Image.FromFile(protokollFileName),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            pictureBox.Show();
            tempYatzyForm.Controls.Add(pictureBox);

            int i;
            Point btnLocation = new Point(200, 0);
            for (i = 0; i < 5; i++)                                 //Creates join buttons
            {
                Button joinButton = new Button()
                {
                    Location = btnLocation,
                    Size = new Size(216, 40),
                    Text = "Join Game",
                    ForeColor = Color.White,
                };

                tempYatzyForm.Controls.Add(joinButton);
                joinButton.Click += new System.EventHandler(PlayerJoinEvent);
                btnLocation.X += joinButton.Size.Width;
            }

            Button btn = new Button
            {
                Size = new Size(100, 100),
                Location = new Point(225 + 216 * i, 500),
                Text = "Kasta tärningarna",
                TabStop = false,
                ForeColor = Color.White,
            };
            btn.Click += Btn_Click_Event;
            tempYatzyForm.Controls.Add(btn);
        }

        private void PlayerJoinEvent(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Hide();
            Player player = new Player();
            tempTable.Join(player);
            SetRowFor(player, btn.Location.X);
        }

        public void SetRowFor(Player player, int senderXLocation)
        {
            PictureBox pictureBox1 = new PictureBox
            {
                Size = new Size(216, 1015),
                Location = new Point(senderXLocation, 0),
                Image = Image.FromFile(kolumnFileName),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            pictureBox1.Show();
            tempYatzyForm.Controls.Add(pictureBox1);

            int increment = 40;
            Point point = new Point(senderXLocation + 10, 95);
            for (int i = 0; i < 20; i++)
            {
                if (i == 6)                 //jumps to bottom part of kolumn
                    point.Y += 83;

                if (i == 13)
                    increment = 39;

                TextBox pointsTextBox = new TextBox()
                {
                    Size = new Size(190, 100),
                    Location = point,
                    Text = "0",
                    Font = new Font(new FontFamily("Arial"), 21, FontStyle.Regular, GraphicsUnit.Pixel),
                    Enabled = false,
                    TabStop = false,
                };

                pointsTextBox.Click += new System.EventHandler(PointsTextBoxOnClick);
                tempYatzyForm.Controls.Add(pointsTextBox);
                player.points.Add(pointsTextBox);
                pointsTextBox.BringToFront();

                point.Y += increment;
            }

            for (int i = 0; i < 4; i++)
            {
                TextBox mscTextBox = CreateMscTextBox(i, senderXLocation + 10, player.name);
                tempYatzyForm.Controls.Add(mscTextBox);
                player.mscTextBoxes.Add(mscTextBox);
                mscTextBox.BringToFront();
            }
        }

        private TextBox CreateMscTextBox(int i, int xPosition, string playerName)
        {
            string name = string.Empty;
            int yPosition = 0;
            string text = string.Empty;
            bool enabled = false;

            switch (i)
            {
                case 0:
                    name = "PlayerName";
                    yPosition = 8;
                    text = playerName;
                    enabled = true;
                    break;

                case 1:
                    name = $"Sum{i}";
                    yPosition = 337;
                    text = "0";
                    enabled = false;
                    break;
                case 2:
                    name = $"Sum{i}";
                    yPosition = 976;
                    text = "0";
                    break;
                case 3:
                    name = "Bonus";
                    yPosition = 379;
                    text = "0";
                    break;
            }
            TextBox mscTextBox = new TextBox()
            {
                Size = new Size(190, 100),
                Location = new Point(xPosition, yPosition),
                Font = new Font(new FontFamily("Arial"), 22, FontStyle.Bold, GraphicsUnit.Pixel),
                Name = name,
                Enabled = enabled,
                Text = text,
                ForeColor = Color.Red,
                TabStop = false,
            };
            return mscTextBox;
        }

        public void PointsTextBoxOnClick(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Name = "Confirmed";
            tb.Enabled = false;

            Player activePlayer = tempTable.SortedPlayerList.First();
            activePlayer.EndTurn(checkList, DieBoxes);
            tempTable.MoveSecondPlayerToFirst(tempTable);

            activePlayer = tempTable.SortedPlayerList.First();
            activePlayer.StartTurn(checkList);
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
                    Image = Image.FromFile(EmptyDieImageFileName),
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
