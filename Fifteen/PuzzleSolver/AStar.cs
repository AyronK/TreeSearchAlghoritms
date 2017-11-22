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
        private int moves = 0;
        private int[] tilesinWrongPlaces = new int[4];
        private int[] tilesToCorrectOrder = new int[4];
        private int[] heuristic = new int[4];
        private int numberOfPossibleMoves = 0;

        public PuzzleSolution Solve(Puzzle unsolved, Puzzle target)
        {
            solution = new PuzzleSolution();
            DateTime startTime = DateTime.Now;

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
                solution.MaxRecursionDepth++;
                moves++;
                var currentState = queue.Dequeue();
                if (!solution.Visited.Contains(currentState))
                {
                    solution.Visited.Add(currentState);
                }

                var possibleMoves = currentState.GetPossibleMoves();
                numberOfPossibleMoves = possibleMoves.Count();
                Puzzle[] possibleNewStates = new Puzzle[4];

                for (int moveId = 0; moveId < searchOrder.Length; moveId++)
                {
                    if (possibleMoves.Contains(searchOrder[moveId]))
                    {
                        var newState = new Puzzle(currentState.ToMatrix());
                        newState.MoveBlank(searchOrder[moveId]);

                        solution.Solution.Add(searchOrder[moveId]);
                        if (newState.Equals(target))
                        {
                            solution.LastState = newState;
                            return;
                        }

                        possibleNewStates[moveId] = newState;
                        solution.Visited.Add(newState);
                        if (heuristicType == "hamm")
                        {
                            tilesinWrongPlaces[moveId] = CountTilesInWrongPlaces(newState, target);
                            heuristic[moveId] = tilesinWrongPlaces[moveId] + moves;
                        }
                        else if(heuristicType == "manh")
                        {
                            tilesToCorrectOrder[moveId] = CountTilesToCorrectOrder(newState, target);
                            heuristic[moveId] = tilesToCorrectOrder[moveId] + moves;
                        }
                    }
                }
                queue.Enqueue(possibleNewStates[FindNextStatesIndex()]);

                solution.Processed.Add(currentState);
            }
        }

        private int CountTilesInWrongPlaces(Puzzle puzzle, Puzzle target)
        {
            int numberOfTiles = 0;
            for(int i = 0; i<puzzle.RowsCount; i++)
            {
                for(int j = 0; j<puzzle.ColumnsCount; j++)
                {
                    if(puzzle.GetValue(i,j) != target.GetValue(i,j))
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

        private int FindNextStatesIndex()
        {
            int min = 0;
            for (int index = 0; index < numberOfPossibleMoves; index++)
            {
                if (heuristic[index] < heuristic[min])
                {
                    min = index;
                }
            }
            return min;
        }
    }
}