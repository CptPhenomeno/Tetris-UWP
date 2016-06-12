using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_S : Tetramino
    {
        public Tetramino_S() : base() { }

        protected override void CreateBlocks()
        {
            blocks.Add(new RectangleBlock(50, 50, 0, 50, Colors.LightSeaGreen, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 0, 100, Colors.LightSeaGreen, Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 50, 50, Colors.LightSeaGreen , Colors.DarkGray, 2));
            blocks.Add(new RectangleBlock(50, 50, 50, 0, Colors.LightSeaGreen, Colors.DarkGray, 2));
        }
    }
}
