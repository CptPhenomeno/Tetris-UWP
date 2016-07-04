using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.GameView.Blocks;
using Windows.UI;

namespace Tetris.GameView.Tetraminoes
{
    public class Tetramino_L : Tetramino
    {
        public Tetramino_L() : base() { }

        protected override void CreateBlocks(int width, int height)
        {
            blocks.Add(new ImageBlock(height, width, 0, 2*width, "orange_block"));           
            blocks.Add(new ImageBlock(height, width, height, 0, "orange_block"));            
            blocks.Add(new ImageBlock(height, width, height, width, "orange_block"));        
            blocks.Add(new ImageBlock(height, width, height, 2*width, "orange_block"));      
        }

        public override void MakeRotation(int rotationType)
        {
            switch (rotationType)
            {
                case 0:
                    blocks[0].MoveLeft();
                    blocks[0].MoveLeft();
                    blocks[1].MoveRightDown();
                    blocks[3].MoveLeftUp();
                    break;
                case 1:
                    blocks[0].MoveDown();
                    blocks[0].MoveDown();
                    blocks[1].MoveRightUp();
                    blocks[3].MoveLeftDown();
                    break;
                case 2:
                    blocks[0].MoveRight();
                    blocks[0].MoveRight();
                    blocks[1].MoveLeftUp();
                    blocks[3].MoveRightDown();
                    break;
                case 3:
                    blocks[0].MoveUp();
                    blocks[0].MoveUp();
                    blocks[1].MoveLeftDown();
                    blocks[3].MoveRightUp();
                    break;
            }
        }
    }
}
