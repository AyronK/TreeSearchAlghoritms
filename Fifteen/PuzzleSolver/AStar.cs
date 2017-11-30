using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public class AStar : IPuzzleSolver
    {
        public AStar()
        {
            searchOrder = new Direction[] { Direction.Left, Direction.Up, Direction.Right, Direction.Down };
        }

        public string heuristicType = null;

        private Direction[] searchOrder = null;

        private PuzzleSolution solution = null;

        private List<int> tilesinWrongPlaces = new List<int>(new int[4]); 
        private List<int> tilesToCorrectOrder = new List<int>(new int[4]);
        private List<int> heuristic = new List<int>(new int[4]);

        public PuzzleSolution Solve(Puzzle unsolved, Puzzle target)
        {
            solution = new PuzzleSolution();
            DateTime startTime = DateTime.Now;

            unsolved.Cost = 0;
            solution.RecursionDepth = 0;
            solution.MaxReachedRecursionDepth = 0;
            Proceed(unsolved, target);

            solution.Duration = DateTime.Now - startTime;
            return solution;
        }

        private void Proceed(Puzzle puzzle, Puzzle target)
        {
            Queue<Puzzle> queue = new Queue<Puzzle>();
            queue.Enqueue(puzzle);

            while (queue.Count != 0)
            {
                solution.MovesMade++;
                heuristic = new List<int> { Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue };

                var currentState = queue.Dequeue();
                solution.Visited.Add(currentState);
                solution.RecursionDepth = currentState.Cost;
                if (solution.MaxReachedRecursionDepth < solution.RecursionDepth)
                {
                    solution.MaxReachedRecursionDepth = solution.RecursionDepth;
                }

                var possibleMoves = currentState.GetPossibleMoves();
                List<Puzzle> possibleNewStates = new List<Puzzle>(new Puzzle[4]); 

                for (int moveId = 0; moveId < searchOrder.Length; moveId++)
                {
                    if (possibleMoves.Contains(searchOrder[moveId]))
                    {
                        var newState = new Puzzle(currentState.ToMatrix());
                        newState.Cost = currentState.Cost + 1;
                        newState.MoveBlank(searchOrder[moveId]);
                        newState.MovesMade = new List<Direction>( currentState.MovesMade);
                        newState.MovesMade.Add(searchOrder[moveId]);


                        if (newState.Equals(target))
                        {
                            solution.LastState = newState;
                            solution.IsSolved = true;
                            solution.Solution.Clear();
                            solution.Solution.AddRange(newState.MovesMade);
                            solution.RecursionDepth = newState.Cost;
                            if (solution.MaxReachedRecursionDepth < solution.RecursionDepth)
                            {
                                solution.MaxReachedRecursionDepth = solution.RecursionDepth;
                            }
                            return;
                        }

                        possibleNewStates[moveId] = newState;

                        if(solution.Visited.Find(el => el.Equals(newState)) == null)
                        {
                            solution.Visited.Add(newState);
                            if (heuristicType == "hamm")
                            {
                                tilesinWrongPlaces[moveId] = CountTilesInWrongPlaces(newState, target);
                                heuristic[moveId] = tilesinWrongPlaces[moveId] + solution.MovesMade;
                            }
                            else if (heuristicType == "manh")
                            {
                                tilesToCorrectOrder[moveId] = CountTilesToCorrectOrder(newState, target);
                                heuristic[moveId] = tilesToCorrectOrder[moveId] + solution.MovesMade;
                            }
                        }
                    }
                }


                    List<int> heuristicSorted = new List<int>(new int[4]);
                    for (int i = 0; i < 4; i++)
                    {
                        heuristicSorted[i] = heuristic[i];
                    }
                    heuristicSorted.Sort();

                    int index = 0;
                    while (index < 4)
                    {
                        int value = heuristicSorted[index];

                        for (int i = 0; i < 4; i++)
                        {
                            if (heuristic[i] == value)
                            {
                                if (possibleNewStates[i] != null)
                                {
                                    queue.Enqueue(possibleNewStates[i]);
                                    //solution.Solution.Add(searchOrder[i]);
                                }
                                index++;
                            }
                        }
                    }


                solution.Processed.Add(currentState);
            }
        }

        private int CountTilesInWrongPlaces(Puzzle puzzle, Puzzle target)
        {
            int numberOfTiles = 0;
            for (int i = 0; i < puzzle.RowsCount; i++)
            {
                for (int j = 0; j < puzzle.ColumnsCount; j++)
                {
                    if (puzzle.GetValue(i, j) != target.GetValue(i, j))
                    {
                        numberOfTiles++;
                    }
                }
            }
            return numberOfTiles;
        }


        private int CountTilesToCorrectOrder(Puzzle puzzle, Puzzle target)
        {
            int movesToMake = 0;
            for (int i = 0; i < puzzle.RowsCount; i++)
            {
                for (int j = 0; j < puzzle.ColumnsCount; j++)
                {
                    if (puzzle.GetValue(i, j) != target.GetValue(i, j))
                    {
                        int iTarget = target.RowOf(puzzle.GetValue(i, j));
                        int jTarget = target.ColumnOf(puzzle.GetValue(i, j));
                        movesToMake += Math.Abs(i - iTarget) + Math.Abs(j - jTarget);
                    }
                }
            }
            return movesToMake;
        }
    }
}