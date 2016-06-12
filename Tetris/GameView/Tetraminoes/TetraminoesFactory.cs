using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.GameView.Tetraminoes
{
    public enum TetraminoShape
    {
        S,
        Z,
        I,
        J,
        L,
        O,
        T
    }

    public class TetraminoesFactory
    {
        public static Tetramino CreateTetramino(TetraminoShape shape)
        {
            switch (shape)
            {
                case TetraminoShape.S:
                    return new Tetramino_S();
                case TetraminoShape.Z:
                    return new Tetramino_Z();
                case TetraminoShape.I:
                    return new Tetramino_I();
                case TetraminoShape.J:
                    return new Tetramino_J();
                case TetraminoShape.L:
                    return new Tetramino_L();
                case TetraminoShape.O:
                    return new Tetramino_O();
                case TetraminoShape.T:
                    return new Tetramino_T();
                default:
                    throw new InvalidTetraminoShape("Does not exist a tetramino with this shape");
            }
        }

        public static Tetramino CreateRandomTetramino()
        {
            Random randomizer = new Random(DateTime.Now.Millisecond);
            int randomShape = randomizer.Next(0, 7);
            return CreateTetramino((TetraminoShape) randomShape);
        }
    }
}
