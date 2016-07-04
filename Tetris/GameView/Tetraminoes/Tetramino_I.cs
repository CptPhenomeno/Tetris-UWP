using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.GameView.Blocks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_I : Tetramino
    {
        public Tetramino_I() : base() { }

        protected override void CreateBlocks(int width, int height)
        {
            blocks.Add(new ImageBlock(height, width, 0, 0, "cyan_block"));
            blocks.Add(new ImageBlock(height, width, 0, width, "cyan_block"));
            blocks.Add(new ImageBlock(height, width, 0, 2*width, "cyan_block"));
            blocks.Add(new ImageBlock(height, width, 0, 3*width, "cyan_block"));
        }

        public override void MakeRotation(int rotationType)
        {
            switch (rotationType)
            {
                case 0:
                    blocks[0].MoveRight();
                    blocks[0].MoveRightUp();
                    blocks[1].MoveRight();
                    blocks[2].MoveDown();
                    blocks[3].MoveDown();
                    blocks[3].MoveLeftDown();
                    break;
                case 1:
                    blocks[0].MoveDown();
                    blocks[0].MoveRightDown();
                    blocks[1].MoveDown();
                    blocks[2].MoveLeft();
                    blocks[3].MoveLeft();
                    blocks[3].MoveLeftUp();
                    break;
                case 2:
                    blocks[0].MoveLeft();
                    blocks[0].MoveLeftDown();
                    blocks[1].MoveLeft();
                    blocks[2].MoveUp();
                    blocks[3].MoveUp();
                    blocks[3].MoveRightUp();
                    break;
                case 3:
                    blocks[0].MoveUp();
                    blocks[0].MoveLeftUp();
                    blocks[1].MoveUp();
                    blocks[2].MoveRight();
                    blocks[3].MoveRight();
                    blocks[3].MoveRightDown();
                    break;
            }
        }
    }
}
