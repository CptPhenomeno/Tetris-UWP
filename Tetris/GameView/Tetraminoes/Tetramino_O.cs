using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.GameView.Blocks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_O : Tetramino
    {
        public Tetramino_O() : base() { }

        protected override void CreateBlocks(int width, int height)
        {
            blocks.Add(new ImageBlock(height, width, 0, 0, "yellow_block"));
            blocks.Add(new ImageBlock(height, width, 0, width, "yellow_block"));
            blocks.Add(new ImageBlock(height, width, height, 0, "yellow_block"));
            blocks.Add(new ImageBlock(height, width, height, width, "yellow_block"));
        }

        public override void Rotate(bool direct) { }
        public override void MakeRotation(int rotationType) { }
    }
}
