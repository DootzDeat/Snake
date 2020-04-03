using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace Snake
{
    partial class GameBoardManager
    {
        #region Properties
        private Form gameBoard = new Form();
        List<Square> snake = new List<Square>();
        Square headSnake = new Square();
        Square food = new Square();

        PictureBox ptbheadSnake = new PictureBox();
        PictureBox ptbfood = new PictureBox();
        List<PictureBox> ptbsnake = new List<PictureBox>();

        PictureBox TopLine = new PictureBox();
        PictureBox BotLine = new PictureBox();
        PictureBox LeftLine = new PictureBox();
        PictureBox RightLine = new PictureBox();


        Timer timeMovingRight = new Timer();
        Timer timeMovingLeft = new Timer();
        Timer timeMovingUp = new Timer();
        Timer timeMovingDown = new Timer();
        Timer timeScore = new Timer();
        Label label = new Label();


        public Form GameBoard { get => gameBoard; set => gameBoard = value; }
        #endregion

        #region Intialize
        public GameBoardManager(Form form1)
        {
            this.gameBoard = form1;
        }
        #endregion

        #region Methods
        public void DrawGameBoard()
        {
            new Setting();

            label.Size = new Size(60, 30);
            label.Location = new Point(30, 30);
            label.Text = "Score: " + Setting.score;
            snake.Add(headSnake);
            ptbsnake.Add(ptbheadSnake);
            headSnake.DrawSquare(ptbheadSnake);
            gameBoard.Controls.Add(ptbheadSnake);
            gameBoard.Controls.Add(label);
            GenerateFood();

            timeMovingRight.Interval = 1000 / Setting.gameSpeed;
            timeMovingLeft.Interval = 1000 / Setting.gameSpeed;
            timeMovingUp.Interval = 1000 / Setting.gameSpeed;
            timeMovingDown.Interval = 1000 / Setting.gameSpeed;

            gameBoard.KeyDown += EventControls;

            timeScore.Tick += EventScoring;
            timeMovingRight.Tick += EventTimeMovingRight;
            timeMovingLeft.Tick += EventTimeMovingLeft;
            timeMovingUp.Tick += EventTimeMovingUp;
            timeMovingDown.Tick += EventTimeMovingDown;

            //timeMovingRight.Enabled = true;

            //Draw Game Area
            TopLine.Location = new Point(0, 0);
            TopLine.Size = new Size(Setting.gameWidth, 15);
            TopLine.BackColor = Color.Red;

            BotLine.Location = new Point(0, Setting.gameHeigh);
            BotLine.Size = new Size(Setting.gameWidth, 15);
            BotLine.BackColor = Color.Red;

            LeftLine.Location = new Point(0, 0);
            LeftLine.Size = new Size(15, Setting.gameHeigh);
            LeftLine.BackColor = Color.Red;

            RightLine.Location = new Point(Setting.gameWidth, 0);
            RightLine.Size = new Size(15, Setting.gameHeigh + 15);
            RightLine.BackColor = Color.Red;

            gameBoard.Controls.Add(TopLine);
            gameBoard.Controls.Add(BotLine);
            gameBoard.Controls.Add(LeftLine);
            gameBoard.Controls.Add(RightLine);

            timeScore.Enabled = true;


        }

        private void EventScoring(object sender, EventArgs e)
        {
            label.Text = "Score: " + Setting.score;
        }

        private void EventTimeMovingRight(object sender, EventArgs e)
        {
            if (ptbheadSnake.Right < Setting.gameWidth)
            {
                gameBoard.Controls.Add(label);
                GameOver();
                EatFood();
                if (ptbsnake.Count == 1)
                {
                    ptbheadSnake.Left += Setting.step;
                }


                if (ptbsnake.Count != 1)
                {
                    for (int i = ptbsnake.Count - 1; i > 0; i--)
                    {
                        ptbsnake[i].Location = new Point(ptbsnake[i - 1].Left, ptbsnake[i - 1].Top);
                    }
                    ptbheadSnake.Left += Setting.step;

                }
                //ptbheadSnake.Left += 10;
            }
        }

        private void EventTimeMovingLeft(object sender, EventArgs e)
        {
            label.Text = "Score: " + Setting.score;

            GameOver();
            EatFood();
            if (ptbheadSnake.Left > 0)
            {
                if (ptbsnake.Count == 1)
                {
                    ptbheadSnake.Left -= Setting.step;
                }

                if (ptbsnake.Count != 1)
                {
                    for (int i = ptbsnake.Count - 1; i > 0; i--)
                    {
                        ptbsnake[i].Location = new Point(ptbsnake[i - 1].Left, ptbsnake[i - 1].Top);
                    }
                    ptbheadSnake.Left -= Setting.step;
                }
                //ptbheadSnake.Left -= 10;
            }
        }
        private void EventTimeMovingUp(object sender, EventArgs e)
        {


            EatFood();
            GameOver();
            if (ptbheadSnake.Top > 0)
            {
                if (ptbsnake.Count == 1)
                {
                    ptbheadSnake.Top -= Setting.step;
                }

                if (ptbsnake.Count != 1)
                {
                    for (int i = ptbsnake.Count - 1; i > 0; i--)
                    {
                        ptbsnake[i].Location = new Point(ptbsnake[i - 1].Left, ptbsnake[i - 1].Top);
                    }
                    ptbheadSnake.Top -= Setting.step;
                }
                //ptbheadSnake.Top -= 10;
            }
        }
        private void EventTimeMovingDown(object sender, EventArgs e)
        {


            if (ptbheadSnake.Bottom < Setting.gameHeigh)
            {
                GameOver();
                EatFood();
                if (ptbsnake.Count == 1)
                {
                    ptbheadSnake.Top += Setting.step;
                }

                if (ptbsnake.Count != 1)
                {
                    for (int i = ptbsnake.Count - 1; i > 0; i--)
                    {
                        ptbsnake[i].Location = new Point(ptbsnake[i - 1].Left, ptbsnake[i - 1].Top);
                    }
                    ptbheadSnake.Top += Setting.step;
                }
                //ptbheadSnake.Top += 10;
            }
        }

        private void EventControls(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && timeMovingRight.Enabled != true)
            {
                timeMovingRight.Enabled = false;
                timeMovingUp.Enabled = false;
                timeMovingDown.Enabled = false;
                timeMovingLeft.Enabled = true;
            }
            if (e.KeyCode == Keys.Right && timeMovingLeft.Enabled != true)
            {
                timeMovingLeft.Enabled = false;
                timeMovingUp.Enabled = false;
                timeMovingDown.Enabled = false;
                timeMovingRight.Enabled = true;
            }
            if (e.KeyCode == Keys.Up && timeMovingDown.Enabled != true)
            {
                timeMovingRight.Enabled = false;
                timeMovingLeft.Enabled = false;
                timeMovingDown.Enabled = false;
                timeMovingUp.Enabled = true;
            }
            if (e.KeyCode == Keys.Down && timeMovingUp.Enabled != true)
            {
                timeMovingRight.Enabled = false;
                timeMovingUp.Enabled = false;
                timeMovingLeft.Enabled = false;
                timeMovingDown.Enabled = true;
            }
        }

        private void GenerateFood()
        {
            Random r = new Random();
            food.xLocation = r.Next(0, 900);
            food.yLocation = r.Next(0, 600);
            food.DrawSquare(ptbfood);
            ptbfood.BackColor = Color.Aqua;
            gameBoard.Controls.Add(ptbfood);
            Setting.gameSpeed += 5;
            Setting.score += Setting.point;
        }
        private void EatFood()
        {
            if (ptbheadSnake.Bounds.IntersectsWith(ptbfood.Bounds))
            {

                GenerateFood();
                snake.Add(new Square());
                ptbsnake.Add(new PictureBox());
                snake[snake.Count - 1].DrawSquare(ptbsnake[ptbsnake.Count - 1]);
                ptbsnake[ptbsnake.Count - 1].BackColor = Color.Black;
                gameBoard.Controls.Add(ptbsnake[ptbsnake.Count - 1]);


                if (ptbsnake.Count != 1 && timeMovingDown.Enabled == true)
                {
                    ptbsnake[ptbsnake.Count - 1].Location = new Point(ptbsnake[ptbsnake.Count - 2].Left, ptbsnake[ptbsnake.Count - 2].Top + ptbheadSnake.Width + 1);
                }
                if (ptbsnake.Count != 1 && timeMovingUp.Enabled == true)
                {
                    ptbsnake[ptbsnake.Count - 1].Location = new Point(ptbsnake[ptbsnake.Count - 2].Left, ptbsnake[ptbsnake.Count - 2].Top - ptbheadSnake.Width - 1);
                }

                if (ptbsnake.Count != 1 && timeMovingLeft.Enabled == true)
                {
                    ptbsnake[ptbsnake.Count - 1].Location = new Point(ptbsnake[ptbsnake.Count - 2].Left + ptbheadSnake.Width + 1, ptbsnake[ptbsnake.Count - 2].Top);
                }

                if (ptbsnake.Count != 1 && timeMovingLeft.Enabled == true)
                {
                    ptbsnake[ptbsnake.Count - 1].Location = new Point(ptbsnake[ptbsnake.Count - 2].Left - ptbheadSnake.Width - 1, ptbsnake[ptbsnake.Count - 2].Top);
                }


            }
        }

        public void GameOver()
        {
            if (ptbsnake.Count > 5)
            {
                for (int i = 4; i < ptbsnake.Count; i++)
                {
                    if (ptbheadSnake.Bounds.IntersectsWith(ptbsnake[i].Bounds))
                    {
                        timeMovingRight.Enabled = false;
                        timeMovingLeft.Enabled = false;
                        timeMovingDown.Enabled = false;
                        timeMovingUp.Enabled = false;

                        MessageBox.Show("Game Over");
                        gameBoard.Close();
                    }
                }
            }
            if (    ptbheadSnake.Bounds.IntersectsWith(TopLine.Bounds)
                    ||ptbheadSnake.Bounds.IntersectsWith(LeftLine.Bounds)
                    )
            {
                timeMovingRight.Enabled = false;
                timeMovingLeft.Enabled = false;
                timeMovingDown.Enabled = false;
                timeMovingUp.Enabled = false;

                MessageBox.Show("Game Over");
                gameBoard.Close();
            }
            if(     ptbheadSnake.Bounds.IntersectsWith(BotLine.Bounds)
                || ptbheadSnake.Bounds.IntersectsWith(RightLine.Bounds))
            {
                timeMovingRight.Enabled = false;
                timeMovingLeft.Enabled = false;
                timeMovingDown.Enabled = false;
                timeMovingUp.Enabled = false;

                MessageBox.Show("Game Over");
                gameBoard.Close();
            }


            #endregion
        }
    }
}
