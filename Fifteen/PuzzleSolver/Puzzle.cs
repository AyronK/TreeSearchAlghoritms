using System;

namespace PuzzleSolver
{
    public class Puzzle
    {
        private byte[,] Elements { get; set; }

        private Tuple<byte, byte> Blank { get; set; }

        public Puzzle(byte[,] elements)
        {
            Elements = elements;
        }

        public int GetValue(byte row, byte column) => Elements[row, column];

        public int this[byte row, byte column] => GetValue(row, column);

        public void MoveBlank(Direction direction)
        {
            throw new NotImplementedException();
        }

        public Direction[] GetPossibleMoves()
        {
            throw new NotImplementedException();
        }

        public Tuple<byte, byte> GetBlankPosition()
        {
            throw new NotImplementedException();
        }

        public byte[,] ToMatrix()
        {
            return Elements;
        }
    }
}
