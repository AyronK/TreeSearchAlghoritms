using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuzzleSolver;
using System.Linq;

namespace FifteenTest
{
    [TestClass]
    public class PuzzleTest
    {
        [TestMethod]
        public void PuzzleInitTest()
        {
            int dimentionLenght = 3;
            Puzzle puzzle = new Puzzle(dimentionLenght);
            int sum = 0;
            foreach (var cell in puzzle.ToArray())
            {
                sum += cell;
            }
            int firstCell = 0;
            int count = (int)Math.Pow(dimentionLenght, 2);
            int lastCell = count - 1;

            Assert.AreEqual(firstCell + lastCell / 2.0 * count, sum);
        }
    }
}
