using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuzzleSolver;
using System.Linq;
using FileManager;

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
            //byte[,] testTab =
            //    {
            //        { 1, 2, 3, 4 },
            //        { 6, 10 , 7, 8},
            //        { 5, 0, 11, 12 },
            //        { 9, 13, 14, 15 }
            //};

            byte[,] testTab =
                {
                    { 0, 1, 3, 4 },
                    { 5, 2 , 7, 8},
                    { 9, 6, 11, 12 },
                    { 13, 10, 14, 15 }
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
        //    FileWriter.WriteSolution(solution, @"C:\Users\Ayron\Desktop\solutionBFS.txt");
        }

        [TestMethod]
        public void DFSTest()
        {
            //byte[,] testTab =
            //    {
            //        { 1, 2, 3, 4 },
            //        { 5, 6 , 7, 8},
            //        { 9, 11, 0, 12 },
            //        { 13, 10, 14, 15 }
            //};

            byte[,] testTab =
                 {
                    { 1, 2, 3, 4 },
                    { 5, 6 , 7, 8},
                    { 0, 9, 11, 12 },
                    { 13, 10, 14, 15 }
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

            DepthFirstSearch dfs = new DepthFirstSearch(new Direction[] { Direction.Down, Direction.Right, Direction.Up, Direction.Left });

            var solution = dfs.Solve(newPuzzle, target);

            Assert.IsTrue(solution.LastState.Equals(target));
            
            //    FileWriter.WriteSolution(solution, @"C:\Users\Ayron\Desktop\solutionBFS.txt");
        }

        [TestMethod]
        public void AStarHammingTest()
        {
            //byte[,] testTab =
            //    {
            //        { 1, 2, 3, 4, },
            //        { 5, 6, 11, 7, },
            //        { 9, 10, 8, 0, },
            //        { 13, 14, 15, 12 }
            //};

            byte[,] testTab =
                {
                    { 1, 2, 3, 4 },
                    { 5, 0, 7, 8},
                    { 9, 6, 11, 12 },
                    { 13, 10, 14, 15 }
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

            AStar astar = new AStar();
            astar.heuristicType = "hamm";

            var solution = astar.Solve(newPuzzle, target);

            Assert.IsTrue(solution.LastState.Equals(target));
            FileWriter.WriteSolution(solution, @"solutionHAMN.txt");
        }


        [TestMethod]
        public void AStarManhattanTest()
        {
            byte[,] testTab =
                {
                    { 1, 2, 3, 4 },
                    { 5, 0, 6, 8},
                    { 9, 11, 7, 12 },
                    { 13, 10, 14, 15 }
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
            AStar astar = new AStar();
            astar.heuristicType = "manh";

            var solution = astar.Solve(newPuzzle, target);

            Assert.IsTrue(solution.LastState.Equals(target));
          //  FileWriter.WriteSolution(solution, @"C:\Users\Ayron\Desktop\solutionMANH.txt");
          //  FileWriter.WriteSolutionDetails(solution, @"C:\Users\Ayron\Desktop\detailsMANH.txt");

        }
    }
}
