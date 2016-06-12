using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_J : Tetramino
    {
        public Tetramino_J() : base() { }

        protected override void CreateBlocks()
        {
            blocks.Add(new RectangleBlock(50, 50, 0, 50, Colors.Green, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 50, 50, Colors.Green, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 100, 0, Colors.Green, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 100, 50, Colors.Green, Colors.DarkGray, 2));
        }
    }
}
