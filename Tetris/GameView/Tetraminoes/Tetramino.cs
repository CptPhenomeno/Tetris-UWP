using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.GameView.Blocks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Tetris.GameView.Tetraminoes
{
    public abstract class Tetramino
    {
        protected List<Block> blocks;
        protected int state;

        public Tetramino()
        {
            blocks = new List<Block>(4);
            CreateBlocks(Tetris.Resources.BlockWidth, Tetris.Resources.BlockHeight);
        }

        public void Fall()
        {
            foreach (Block b in blocks)
                b.MoveDown();
        }

        public virtual void Rotate(bool direct)
        {
            int rotationType = (direct) ? state : (state + 1) % 4;
            MakeRotation(rotationType);
            state = (direct) ? ((state + 1) % 4) : ((state == 0) ? 3 : (state - 1) % 4);
        }

        public abstract void MakeRotation(int rotationType);

        public void MoveLeft()
        {
            //kick wall
            foreach (Block b in blocks)
                b.MoveLeft();
        }

        public void MoveRight()
        {
            //kick wall
            foreach (Block b in blocks)
                b.MoveRight();
        }
        
        public void Draw(Canvas canvas)
        {
            foreach (Block b in blocks)
                b.DrawBlock(canvas);
        }
       
        protected abstract void CreateBlocks(int width, int height);

        public List<Block> GetBlocks()
        {
            return blocks;
        }

        public int[] GetBlocksCoordinates()
        {
            int[] coord = new int[8];
            int i = 0;

            foreach (Block b in blocks)
            {
                coord[i] = b.GetTop();
                coord[i + 1] = b.GetLeft();
                i += 2;
            }

            return coord;
        }

        public int GetBlockWidth()
        {
            return blocks[0].GetWidth();
        }

        public int GetBlockHeight()
        {
            return blocks[0].GetHeight();
        }
        
    }
}
