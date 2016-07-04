using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Tetris.GameView.Blocks
{
    public class ImageBlock : Block
    {
        private Image image;

        public ImageBlock(int width, int height, int top, int left, string imageName)
            :base (top, left, width, height)
        {
            image = new Image();
            InitImage(imageName);
        }

        private void InitImage(string imageName)
        {
            image.Source = new BitmapImage(new Uri("ms-appx:///Assets/"+imageName+".png"));            
            image.Width = width;
            image.Height = height;
            image.SetValue(Canvas.LeftProperty, left);
            image.SetValue(Canvas.TopProperty, top);
        }

        public override void MoveLeft()
        {
            base.MoveLeft();
            image.SetValue(Canvas.LeftProperty, left);
        }

        public override void MoveRight()
        {
            base.MoveRight();
            image.SetValue(Canvas.LeftProperty, left);
        }

        public override void MoveUp()
        {
            base.MoveUp();
            image.SetValue(Canvas.TopProperty, top);
        }

        public override void MoveDown()
        {
            base.MoveDown();
            image.SetValue(Canvas.TopProperty, top);
        }

        public override void DrawBlock(Canvas canvas)
        {
            canvas.Children.Add(image);
        }

        public override void DeleteBlock(Canvas canvas)
        {
            canvas.Children.Remove(image);
        }
    }
}
