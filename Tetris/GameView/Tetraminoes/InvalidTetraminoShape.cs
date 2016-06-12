using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.GameView.Tetraminoes
{
    public class InvalidTetraminoShape : ArgumentException
    {
        public InvalidTetraminoShape() : base() { }
        public InvalidTetraminoShape(string s) : base(s) { }
    }
}
