using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace Tetris.GameView.Blocks
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

        #region Basic movement
        public virtual void MoveLeft()
        {
            left -= width;
        }

        public virtual void MoveRight()
        {
            left += width;
        }

        public virtual void MoveUp()
        {
            top -= height;
        }

        public virtual void MoveDown()
        {
            top += height;
        }
        #endregion

        #region Composite movement
        public void MoveLeftUp()
        {
            MoveLeft();
            MoveUp();
        }

        public void MoveLeftDown()
        {
            MoveLeft();
            MoveDown();
        }

        public void MoveRightUp()
        {
            MoveRight();
            MoveUp();
        }

        public void MoveRightDown()
        {
            MoveRight();
            MoveDown();
        }
        #endregion

        public abstract void DrawBlock(Canvas canvas);

        public abstract void DeleteBlock(Canvas canvas);

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
