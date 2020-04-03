using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        #region Properties
        GameBoardManager gameBoard;
        #endregion
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            gameBoard = new GameBoardManager(f);
            f.Size = new Size(2000, 1000);
            f.BackColor = Color.LightPink;
            f.Show();
            gameBoard.DrawGameBoard();
        }
    }
}
