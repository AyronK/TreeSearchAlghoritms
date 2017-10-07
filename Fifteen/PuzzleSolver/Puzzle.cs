using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public class Puzzle
    {
        private static Random random = new Random();

        private int[,] puzzle;

        public Puzzle(int dimentionLenght)
        {
            puzzle = new int[dimentionLenght, dimentionLenght];

            int[] values = new int[dimentionLenght * dimentionLenght];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = i;
            }
            values = values.OrderBy(x => random.Next(0, dimentionLenght)).ToArray();

            for (int i = 0; i < values.Length; i++)
                puzzle[i / dimentionLenght, i % dimentionLenght] = values[i];
        }

        public int GetValue(int row, int column) => puzzle[row, column];

        public int this[int row, int column] => GetValue(row, column);

        public int[,] ToArray()
        {
            return puzzle;
        }
    }
}
