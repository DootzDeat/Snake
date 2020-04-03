using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Setting
    {
        public static int gameSpeed { get; set; }
        public static int gameWidth { get; set; }
        public static int gameHeigh { get; set; }
        public static int step { get; set; }
        public static int point { get; set; }
        public static int score { get; set; }
        public static bool GameOver { get; set; }

        public Setting()
        {
            score = -100;
            gameWidth = 1500;
            gameHeigh = 768;
            step = 30;
            gameSpeed = 20;
            point = 100;
            GameOver = false;
        }

    }
}
