using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_L : Tetramino
    {
        public Tetramino_L() : base() { }

        protected override void CreateBlocks()
        {
            blocks.Add(new RectangleBlock(50, 50, 0, 0, Colors.LightGreen, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 50, 0, Colors.LightGreen, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 100, 0, Colors.LightGreen, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 100, 50, Colors.LightGreen, Colors.DarkGray, 2));
        }
    }
}
