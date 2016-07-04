using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.GameView.Blocks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_T : Tetramino
    {
        public Tetramino_T() : base() { }

        protected override void CreateBlocks(int width, int height)
        {
            blocks.Add(new ImageBlock(Tetris.Resources.BlockHeight, Tetris.Resources.BlockWidth, 0, width, "purple_block"));
            blocks.Add(new ImageBlock(Tetris.Resources.BlockHeight, Tetris.Resources.BlockWidth, height, 0, "purple_block"));
            blocks.Add(new ImageBlock(Tetris.Resources.BlockHeight, Tetris.Resources.BlockWidth, height, width, "purple_block"));
            blocks.Add(new ImageBlock(Tetris.Resources.BlockHeight, Tetris.Resources.BlockWidth, height, 2*width, "purple_block"));
        }

        public override void MakeRotation(int rotationType)
        {
            switch (rotationType)
            {
                case 0:
                    blocks[0].MoveRightDown();
                    blocks[1].MoveRightUp();
                    blocks[3].MoveLeftDown();
                    break;
                case 1:
                    blocks[0].MoveLeftDown();
                    blocks[1].MoveRightDown();
                    blocks[3].MoveLeftUp();
                    break;
                case 2:
                    blocks[0].MoveLeftUp();
                    blocks[1].MoveLeftDown();
                    blocks[3].MoveRightUp();
                    break;
                case 3:
                    blocks[0].MoveRightUp();
                    blocks[1].MoveLeftUp();
                    blocks[3].MoveRightDown();
                    break;
            }
        }
    }
}
