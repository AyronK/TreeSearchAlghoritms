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
            byte[,] testTab = { { 0, 1 }, { 2, 3 } };
            Puzzle newPuzzle = new Puzzle(testTab);

            for (int i = 0; i < newPuzzle.RowsCount; i++)
            {
                for (int j = 0; j < newPuzzle.ColumnsCount; j++)
                {
                    Assert.AreEqual(testTab[i, j], newPuzzle[i, j]);
                }
            }
        }

        [TestMethod]
        public void PossibleDirectionsCornerTest()
        {
            byte[,] testTab =
                {
                    { 0, 1, 2 },
                    { 3, 4, 5 }
                };
            Puzzle newPuzzle = new Puzzle(testTab);

            var possibleDIrections = newPuzzle.GetPossibleMoves();
            Assert.IsTrue(possibleDIrections.Count() == 2);
            Assert.IsTrue(possibleDIrections.Contains(Direction.Down));
            Assert.IsTrue(possibleDIrections.Contains(Direction.Right));
        }

        [TestMethod]
        public void PossibleDirectionsCenterTest()
        {
            byte[,] testTab =
                {
                    { 4, 1, 2 },
                    { 3, 0, 5 },
                    { 6, 7, 8 }
                };
            Puzzle newPuzzle = new Puzzle(testTab);

            var possibleDIrections = newPuzzle.GetPossibleMoves();
            Assert.IsTrue(possibleDIrections.Count() == 4);
            Assert.IsTrue(possibleDIrections.Contains(Direction.Down));
            Assert.IsTrue(possibleDIrections.Contains(Direction.Right));
            Assert.IsTrue(possibleDIrections.Contains(Direction.Up));
            Assert.IsTrue(possibleDIrections.Contains(Direction.Left));
        }

        [TestMethod]
        public void PossibleDirectionsEdgeTest()
        {
            byte[,] testTab =
                {
                    { 1, 0, 2 },
                    { 3, 4, 5 }
                };
            Puzzle newPuzzle = new Puzzle(testTab);

            var possibleDIrections = newPuzzle.GetPossibleMoves();
            Assert.IsTrue(possibleDIrections.Count() == 3);
            Assert.IsTrue(possibleDIrections.Contains(Direction.Down));
            Assert.IsTrue(possibleDIrections.Contains(Direction.Right));
            Assert.IsTrue(possibleDIrections.Contains(Direction.Left));
        }

        [TestMethod]
        public void PossibleDirectionsBottomEdgeTest()
        {
            byte[,] testTab =
                {
                    { 1, 4, 2 },
                    { 8, 6, 7 },
                    { 3, 0, 5 }
                };
            Puzzle newPuzzle = new Puzzle(testTab);

            var possibleDIrections = newPuzzle.GetPossibleMoves();
            Assert.IsTrue(possibleDIrections.Count() == 3);
            Assert.IsTrue(possibleDIrections.Contains(Direction.Up));
            Assert.IsTrue(possibleDIrections.Contains(Direction.Right));
            Assert.IsTrue(possibleDIrections.Contains(Direction.Left));
        }

        [TestMethod]
        public void MoveBlankCanMoveTest()
        {
            byte[,] testTab =
                {
                    { 1, 0, 2 },
                    { 3, 4, 5 }
            };
            Puzzle newPuzzle = new Puzzle(testTab);

            var oldBlankColumn = newPuzzle.ColumnOfBlank;
            newPuzzle.MoveBlank(Direction.Right);

            Assert.AreEqual(oldBlankColumn + 1, newPuzzle.ColumnOfBlank);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MoveBlankCannotMoveTest()
        {
            byte[,] testTab =
                {
                    { 1, 0, 2 },
                    { 3, 4, 5 }
            };
            Puzzle newPuzzle = new Puzzle(testTab);
            newPuzzle.MoveBlank(Direction.Up);
        }

        [TestMethod]
        public void CompareToTest()
        {
            byte[,] testTab =
                {
                    { 1, 0, 2 },
                    { 3, 4, 5 }
            };
            Puzzle firstSamePuzzle = new Puzzle(testTab);
            Puzzle secondSamePuzzle = new Puzzle(testTab);

            byte[,] differentTab =
                {
                    { 1, 5, 2 },
                    { 3, 4, 0 }
            };
            Puzzle differentPuzzle = new Puzzle(differentTab);

            Assert.IsTrue(firstSamePuzzle.Equals(secondSamePuzzle));
            Assert.IsFalse(firstSamePuzzle.Equals(differentPuzzle));
            Assert.IsFalse(firstSamePuzzle.Equals(differentPuzzle));
        }

        [TestMethod]
        public void BFSTest()
        {
            byte[,] testTab =
                {
                    { 1, 2, 3, 4 },
                    { 6, 10 , 7, 8},
                    { 5, 0, 11, 12 },
                    { 9, 13, 14, 15 }
            };
            byte[,] targetTab =
                {
                    { 1, 2, 3, 4 },
                    { 5, 6 , 7, 8},
                    { 9, 10, 11, 12 },
                    { 13, 14, 15, 0 }
            };
            Puzzle newPuzzle = new Puzzle(testTab);
            Puzzle target = new Puzzle(targetTab);

            BreadthFirstSearch bfs = new BreadthFirstSearch(new Direction[] { Direction.Right, Direction.Left, Direction.Up, Direction.Down });

            var solution = bfs.Solve(newPuzzle, target);

            Assert.IsTrue(solution.LastState.Equals(target));
        }
    }
}
