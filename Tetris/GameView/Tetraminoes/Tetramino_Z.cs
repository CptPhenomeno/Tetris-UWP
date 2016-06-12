using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_Z : Tetramino
    {
        public Tetramino_Z() : base() { }

        protected override void CreateBlocks()
        {
            blocks.Add(new RectangleBlock(50, 50, 0, 0, Colors.DodgerBlue, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 0, 50, Colors.DodgerBlue, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 50, 50, Colors.DodgerBlue, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 50, 100, Colors.DodgerBlue, Colors.DarkGray, 2));
        }
    }
    
}
