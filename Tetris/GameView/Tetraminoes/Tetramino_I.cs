using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_I : Tetramino
    {
        public Tetramino_I() : base() { }

        protected override void CreateBlocks()
        {
            blocks.Add(new RectangleBlock(50, 50, 0, 0, Colors.Purple, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 50, 0, Colors.Purple, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 100, 0, Colors.Purple, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 150, 0, Colors.Purple, Colors.DarkGray, 2));
        }
    }
}
