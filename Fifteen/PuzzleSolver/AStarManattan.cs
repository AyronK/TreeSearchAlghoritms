using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    class AStarManattan : IPuzzleSolver
    {
        public AStarManattan()
        {
            searchOrder = new Direction[] { Direction.Left, Direction.Up, Direction.Right, Direction.Down };
        }

        private Direction[] searchOrder = null;

        private PuzzleSolution solution = null;

        private int moves = 0;
        private int[] tilesToCorrectOrder = null;
        private int[] manhattanHeuristic = null;


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
                for (int moveId = 0; moveId < searchOrder.Length; moveId++)
                {
                    Puzzle[] possibleNewStates = null;

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
                        tilesToCorrectOrder[moveId] = CountTilesToCorrectOrder(newState, target);
                        manhattanHeuristic[moveId] = tilesToCorrectOrder[moveId] + moves;
                    }

                    // min z manhattanHeuristic i z tej pozycji 
                    queue.Enqueue(possibleNewStates[0]); //
                }
                solution.Processed.Add(currentState);
            }
        }

        private int CountTilesToCorrectOrder(Puzzle puzzle, Puzzle target)
        {
            return 0;
        }
    }
}