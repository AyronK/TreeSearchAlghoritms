using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver
{
    public class Puzzle
    {
        public readonly static byte Blank = 0;

        #region Constructors
        public Puzzle(byte[,] elements)
        {
            RowsCount = elements.GetLength(0);
            ColumnsCount = elements.GetLength(1);
            Elements.AddRange(Enumerable.Repeat(Byte.MaxValue, elements.Length));
            FillPuzzle(elements);
        }

        private void FillPuzzle(byte[,] elements)
        {
            for (int row = 0; row < RowsCount; row++)
            {
                for (int column = 0; column < ColumnsCount; column++)
                {
                    SetValue(row, column, elements[row, column]);
                }
            }
        }
        #endregion

        private List<byte> Elements { get; set; } = new List<byte>();

        public int RowsCount { get; private set; }
        public int ColumnsCount { get; private set; }
        public int RowOfBlank { get; private set; }
        public int ColumnOfBlank { get; private set; }

        public int Cost { get; set; } // do zapisywania głębokości rekursji 

        public byte this[int row, int column] => GetValue(row, column);

        public byte GetValue(int row, int column)
        {
            if (row > RowsCount || column > ColumnsCount)
                throw new IndexOutOfRangeException();
            int index = ConvertPositionToIndex(row, column);
            return Elements[index];
        }

        private void SetValue(int row, int column, byte value)
        {
            if (row > RowsCount || column > ColumnsCount)
                throw new IndexOutOfRangeException();
            int index = ConvertPositionToIndex(row, column);
            Elements[index] = value;
            if (value == Blank)
            {
                RowOfBlank = RowOf(Blank);
                ColumnOfBlank = ColumnOf(Blank);
            }
        }

        private int ConvertPositionToIndex(int row, int column)
        {
            return row * ColumnsCount + column;
        }

        public int RowOf(byte element)
        {
            if (!Elements.Contains(element))
                throw new ArgumentException("That element is not in puzzle");
            return Elements.IndexOf(element) / ColumnsCount;
        }

        public int ColumnOf(byte element)
        {
            if (!Elements.Contains(element))
                throw new ArgumentException("That element is not in puzzle");
            return Elements.IndexOf(element) % ColumnsCount;
        }

        public void MoveBlank(Direction direction)
        {
            if (!GetPossibleMoves().Contains(direction))
            {
                throw new InvalidOperationException("Cannot move in that direction");
            }

            int swappedRow = RowOfBlank, swappedColumn = ColumnOfBlank;

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

            SetValue(RowOfBlank, ColumnOfBlank, this[swappedRow, swappedColumn]);
            SetValue(swappedRow, swappedColumn, Blank);
        }

        public IEnumerable<Direction> GetPossibleMoves()
        {
            List<Direction> possibleMoves = new List<Direction>();

            if (RowOfBlank != 0)
            {
                possibleMoves.Add(Direction.Up);
            }
            if (RowOfBlank != RowsCount - 1)
            {
                possibleMoves.Add(Direction.Down);
            }
            if (ColumnOfBlank != 0)
            {
                possibleMoves.Add(Direction.Left);
            }
            if (ColumnOfBlank != ColumnsCount - 1)
            {
                possibleMoves.Add(Direction.Right);
            }

            return possibleMoves;
        }

        public byte[,] ToMatrix()
        {
            byte[,] result = new byte[RowsCount, ColumnsCount];
            for (int row = 0; row < RowsCount; row++)
            {
                for (int column = 0; column < ColumnsCount; column++)
                {
                    result[row, column] = GetValue(row, column);
                }
            }
            return result;
        }

        public bool Equals(Puzzle puzzle)
        {
            for (int row = 0; row < RowsCount; row++)
            {
                for (int column = 0; column < ColumnsCount; column++)
                {
                    if (this[row, column] != puzzle[row, column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
