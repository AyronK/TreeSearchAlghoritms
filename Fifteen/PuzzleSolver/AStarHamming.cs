using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public class AStarHamming : IPuzzleSolver
    {
        public AStarHamming()
        {
            searchOrder = new Direction[] { Direction.Left, Direction.Up, Direction.Right, Direction.Down };
        }

        private Direction[] searchOrder = null;

        private PuzzleSolution solution = null;

        private int moves = 0;
        private int[] tilesinWrongPlaces = new int[4];
        private int[] hammingHeuristic = new int[4];
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
                solution.Visited.Add(currentState);

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
                        tilesinWrongPlaces[moveId] = CountTilesInWrongPlaces(newState, target);
                        hammingHeuristic[moveId] = tilesinWrongPlaces[moveId] + moves;
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

        private int FindNextStatesIndex()
        {
            int min = 0;
            for (int index = 0; index < numberOfPossibleMoves; index++)
            {
                if (hammingHeuristic[index] < hammingHeuristic[min])
                {
                    min = index;
                }
            }
            return min;
        }
    }
}