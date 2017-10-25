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
            byte[,] testTab = { { 1, 2 }, { 3, 4 } };
            Puzzle newPuzzle = new Puzzle(testTab);

            for (int i = 0; i < newPuzzle.RowsCount; i++)
            {
                for (int j = 0; j < newPuzzle.ColumnsCount; j++)
                {
                    Assert.AreEqual(testTab[i, j], newPuzzle[i, j]);
                }
            }
        }
    }
}
