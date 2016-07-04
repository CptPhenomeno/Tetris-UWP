using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.GameView.Blocks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_S : Tetramino
    {
        public Tetramino_S() : base() { }

        protected override void CreateBlocks(int width, int height)
        {
            blocks.Add(new ImageBlock(Tetris.Resources.BlockHeight, Tetris.Resources.BlockWidth, 0, width, "green_block"));
            blocks.Add(new ImageBlock(Tetris.Resources.BlockHeight, Tetris.Resources.BlockWidth, 0, 2*width, "green_block"));
            blocks.Add(new ImageBlock(Tetris.Resources.BlockHeight, Tetris.Resources.BlockWidth, height, 0, "green_block"));
            blocks.Add(new ImageBlock(Tetris.Resources.BlockHeight, Tetris.Resources.BlockWidth, height, width, "green_block"));
        }

        public override void MakeRotation(int rotationType)
        {
            switch (rotationType)
            {
                case 0:
                    blocks[0].MoveRightDown();
                    blocks[1].MoveDown();
                    blocks[1].MoveDown();
                    blocks[2].MoveRightUp();
                    break;
                case 1:
                    blocks[0].MoveLeftDown();
                    blocks[1].MoveLeft();
                    blocks[1].MoveLeft();
                    blocks[2].MoveRightDown();
                    break;
                case 2:
                    blocks[0].MoveLeftUp();
                    blocks[1].MoveUp();
                    blocks[1].MoveUp();
                    blocks[2].MoveLeftDown();
                    break;
                case 3:
                    blocks[0].MoveRightUp();
                    blocks[1].MoveRight();
                    blocks[1].MoveRight();
                    blocks[2].MoveLeftUp();
                    break;
            }
        }        
    }
}
