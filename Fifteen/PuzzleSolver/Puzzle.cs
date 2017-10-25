using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver
{
    public class Puzzle
    {
        private List<byte> Elements { get; set; } = new List<byte>();
        private Tuple<byte, byte> Blank { get; set; }

        public int RowsCount { get; private set; }
        public int ColumnsCount { get; private set; }

        public Puzzle(byte[,] elements)
        {
            RowsCount = elements.GetLength(0);
            ColumnsCount = elements.GetLength(1);

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    Elements.Add(elements[i, j]);
                }
            }

            IndexOfBlank = IndexOf(0);
        }

        public byte GetValue(int row, int column)
        {
            if (row > RowsCount || column > ColumnsCount)
                throw new IndexOutOfRangeException();
            int position = ConvertPosition(row, column);
            return Elements[position];
        }

        private void SetValue(int row, int column, byte value)
        {
            if (row > RowsCount || column > ColumnsCount)
                throw new IndexOutOfRangeException();
            int position = ConvertPosition(row, column);
            Elements[position] = value;
            if (value == 0)
            {
                IndexOfBlank = new Tuple<int, int>(row, column);
            }
        }

        private int ConvertPosition(int row, int column)
        {
            return row * ColumnsCount + column;
        }

        public Tuple<int, int> IndexOf(byte element)
        {
            return new Tuple<int, int>(RowOf(element), ColumnOf(element));
        }

        private int RowOf(byte element)
        {
            if (!Elements.Contains(element))
                throw new ArgumentException("That element is not in puzzle");
            return Elements.IndexOf(element) / ColumnsCount;
        }

        private int ColumnOf(byte element)
        {
            if (!Elements.Contains(element))
                throw new ArgumentException("That element is not in puzzle");
            return Elements.IndexOf(element) % ColumnsCount;
        }

        public byte this[int row, int column] => GetValue(row, column);

        public void MoveBlank(Direction direction)
        {
            if (!GetPossibleMoves().Contains(direction))
            {
                throw new InvalidOperationException("Cannot move in that direction");
            }

            int swappedRow = IndexOfBlank.Item1, swappedColumn = IndexOfBlank.Item2;

            switch (direction)
            {
                case Direction.Up:
                    swappedRow--;
                    break;
                case Direction.Down:
                    swappedRow++;
                    break;
                case Direction.Left:
                    swappedColumn--;
                    break;
                case Direction.Right:
                    swappedColumn++;
                    break;
            }

            SetValue(IndexOfBlank.Item1, IndexOfBlank.Item2, this[swappedRow, swappedColumn]);
            SetValue(swappedRow, swappedColumn, 0);
        }

        public IEnumerable<Direction> GetPossibleMoves()
        {
            List<Direction> possibleMoves = new List<Direction>();

            if (IndexOfBlank.Item1 != 0)
            {
                possibleMoves.Add(Direction.Up);
            }
            if (IndexOfBlank.Item1 != RowsCount - 1)
            {
                possibleMoves.Add(Direction.Down);
            }
            if (IndexOfBlank.Item2 != 0)
            {
                possibleMoves.Add(Direction.Left);
            }
            if (IndexOfBlank.Item1 != ColumnsCount - 1)
            {
                possibleMoves.Add(Direction.Right);
            }

            return possibleMoves;
        }

        public Tuple<int, int> IndexOfBlank { get; private set; }

        public byte[,] ToMatrix()
        {
            byte[,] result = new byte[RowsCount, ColumnsCount];
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    result[i, j] = GetValue(i, j);
                }
            }
            return result;
        }
    }
}
