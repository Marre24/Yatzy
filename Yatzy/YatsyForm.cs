using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp;
namespace Yatzy
{
    public partial class YatsyForm : Form
    {
        public readonly Table table = new Table();


        public List<Player> playerList = new List<Player>();

        readonly Player player = new Player();
        readonly Player player2 = new Player();
        readonly Player player3 = new Player();

        public YatsyForm()
        {
            InitializeComponent();

            

            if (Connection.Setup())
            {
                MessageBox.Show("Connected");
            }
            else
            {
                MessageBox.Show("Could not connect to ");
            }

            table.Join(player);
            table.Join(player2);



            

            //table.Join(player3);
            table.SetTableIn(this, table);

            player.UpdatePointsFromDatabase();

            player.StartTurn();
            BackColor = Color.DarkGray;
            Text = "Maxi-Yatzy";
        }


        private Size oldSize;
        private void YatsyForm_Load(object sender, EventArgs e) => oldSize = base.Size;

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            foreach (Control cnt in this.Controls)
                ResizeAll(cnt, base.Size);

            oldSize = base.Size;
        }
        private void ResizeAll(Control control, Size newSize)
        {
            int width = newSize.Width - oldSize.Width;
            control.Left += (control.Left * width) / oldSize.Width;
            control.Width += (control.Width * width) / oldSize.Width;

            int height = newSize.Height - oldSize.Height;
            control.Top += (control.Top * height) / oldSize.Height;
            control.Height += (control.Height * height) / oldSize.Height;

        }
    }
}
