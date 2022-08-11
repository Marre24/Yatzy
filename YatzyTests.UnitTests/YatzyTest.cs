using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yatzy;

namespace YatzyTests.UnitTests
{
    [TestClass]
    public class YatzyTest
    {
        [TestMethod]
        public void EndTurn_84Points_BonusAchieved()
        {
            Player p = new Player();
            for (int i = 0; i < 20; i++)
            {
                TextBox tb = new TextBox()
                {
                    Text = "0",
                    Enabled = false,
                    Visible = true,
                    TabStop = false,
                };
                p.points.Add(tb);
            }
            p.mscTextBoxes.Add(new TextBox()
            {
                Text = "0",
                Name = "Bonus",
            });  
            p.points[0].Text = "84";
            p.EndTurn();
            
            Assert.AreEqual("100", p.BonusTextBox.Text);
        }
    }
}
