using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Tetris.GameView.Tetraminoes
{
    public class RectangleBlock : Block
    {
        private Rectangle rect;
        private SolidColorBrush fillBrush;
        private SolidColorBrush strokeBrush;
        private int strokeThickness;

        public RectangleBlock (int width, int height, int top, int left, Color fillColor, Color strokeColor, int strokeThickness)
            :base (top, left, width, height)
        {
            this.strokeThickness = strokeThickness;
            fillBrush = new SolidColorBrush(fillColor);
            strokeBrush = new SolidColorBrush(strokeColor);
            rect = new Rectangle();
            InitRectangle();  
        }

        private void InitRectangle()
        {
            rect.Width = width;
            rect.Height = height;
            rect.Fill = fillBrush;
            rect.Stroke = strokeBrush;
            rect.StrokeThickness = strokeThickness;
            rect.SetValue(Canvas.LeftProperty, left);
            rect.SetValue(Canvas.TopProperty, top);
        }

        public override void MoveHorizontally(int dx) 
        {
            base.MoveHorizontally(dx);
            rect.SetValue(Canvas.LeftProperty, left);
        }

        public override void MoveVertically(int dy)
        {
            base.MoveVertically(dy);
            rect.SetValue(Canvas.TopProperty, top);
        }

        public override void DrawBlock(Canvas canvas)
        {
            //await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            //{
            //    Canvas canvas = view.GetCanvas();
            canvas.Children.Add(rect);
            //});
        }
    }
}
