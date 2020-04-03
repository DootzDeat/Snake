using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Drawing;

namespace Snake
{
    class Square
    {
        public int xLocation { get; set; }
        public int yLocation { get; set; }
        private int squareWidth { get; set; }
        private int squareHeight { get; set; }

        public Square()
        {
            xLocation = 450;
            yLocation = 300;
            squareHeight = 30;
            squareWidth = 30;
        }

        public void DrawSquare(PictureBox ptb)
        {
            ptb.Size = new Size(squareWidth, squareHeight);
            ptb.Location = new Point(xLocation, yLocation);
            ptb.BackColor = Color.Red;

        }


    }
}
