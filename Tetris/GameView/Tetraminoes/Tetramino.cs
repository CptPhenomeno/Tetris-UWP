using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Tetris.GameView.Tetraminoes
{
    public abstract class Tetramino
    {
        protected List<Block> blocks;

        public Tetramino()
        {
            blocks = new List<Block>(4);
            CreateBlocks();
        }

        public void Fall()
        {
            foreach (Block b in blocks)
                b.MoveVertically(b.GetHeight());
        }

        public void MoveLeft()
        {
            //kick wall
            foreach (Block b in blocks)
                b.MoveHorizontally(-b.GetWidth());
        }

        public void MoveRight()
        {
            //kick wall
            foreach (Block b in blocks)
                b.MoveHorizontally(b.GetWidth());
        }

        public void Draw(Canvas canvas)
        {
            foreach (Block b in blocks)
                b.DrawBlock(canvas);
        }
       
        protected abstract void CreateBlocks();

        public List<Block> GetBlocks()
        {
            return blocks;
        }

        public List<Tuple<int,int>> GetBlocksCoordinates()
        {
            List<Tuple<int, int>> list = new List<Tuple<int, int>>(4);

            foreach (Block b in blocks)
            {
                list.Add(new Tuple<int, int>(b.GetTop(), b.GetLeft()));
            }

            return list;
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
