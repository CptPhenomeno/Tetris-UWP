using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Tetris.GameView
{
    public abstract class Block
    {
        protected int top;
        protected int left;
        protected int width;
        protected int height;

        public Block(int top, int left, int width, int height)
        {
            this.top = top;
            this.left = left;
            this.width = width;
            this.height = height;
        }
        
        public virtual void MoveHorizontally(int dx)
        {
            left += dx;
        }

        public virtual void MoveVertically(int dy)
        {
            top += dy;
        }

        public abstract void DrawBlock(Canvas canvas);

        #region Getter
        public int GetTop()
        {
            return top;
        }

        public int GetLeft()
        {
            return left;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }
        #endregion
    }
}
