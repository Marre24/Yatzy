using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Yatzy
{
    public class Canvas
    {
        public string OutcomeFileName = "YatzyProtokoll.jpg";
        public string KolumnFileName = "YatzyKolumn.jpg";
        public TextBox[,] grid = TextBox[2,4];

        public Canvas()
        {

        }

        public void PaintCanvas(Form form)
        {
            Point point = new Point(0, 0);
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(216, 1000),
                Location = point,
                Image = Image.FromFile(OutcomeFileName),
                SizeMode = PictureBoxSizeMode.StretchImage

            };
            pictureBox.Show();
            form.Controls.Add(pictureBox);
            int x = pictureBox.Size.Width;
            //foreach (Player player in )
            {
                point.X += x;
                PictureBox pictureBox1 = new PictureBox
                {
                    Size = new Size(216, 1000),
                    Location = point,
                    Image = Image.FromFile(OutcomeFileName),
                    SizeMode = PictureBoxSizeMode.StretchImage

                };
                pictureBox1.Show();
                form.Controls.Add(pictureBox1);

                for (int i = 0; i < 13; i++)
                {

                    TextBox textBox = new TextBox
                    {
                        Size = new Size(216, 1000),
                        Location = point
                    };
                    point
                }
                


            }

        }




    }
}
