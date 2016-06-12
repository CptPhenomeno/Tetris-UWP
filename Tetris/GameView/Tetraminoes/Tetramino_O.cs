using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_O : Tetramino
    {
        public Tetramino_O() : base() { }
        
        protected override void CreateBlocks()
        {
            blocks.Add(new RectangleBlock(50, 50, 0, 0, Colors.IndianRed, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 0, 50, Colors.IndianRed, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 50, 0, Colors.IndianRed, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 50, 50, Colors.IndianRed, Colors.DarkGray, 2));
        }
    }
}
