using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public class DijkstraAlgorithm : IPuzzleSolver
    {
        private PuzzleSolution solution = null;
        private Direction[] searchOrder = new Direction[4];

        public DijkstraAlgorithm()
        {
            searchOrder = new Direction[] { Direction.Left, Direction.Up, Direction.Right, Direction.Down };
        }

        public PuzzleSolution Solve(Puzzle unsolved, Puzzle target)
        {
            throw new NotImplementedException();
        }

        private void Proceed(Puzzle puzzle, Puzzle target)
        {
            Queue<Puzzle> queue = new Queue<Puzzle>();
            queue.Enqueue(puzzle);
            puzzle.Cost = 0;
            while (queue.Count != 0)
            {
                var currentState = queue.Dequeue();
                solution.Visited.Add(currentState);
                if (currentState.Equals(target))
                {
                    solution.LastState = currentState;
                    solution.IsSolved = true;
                    if (solution.MaxReachedRecursionDepth < currentState.Cost)
                    {
                        solution.MaxReachedRecursionDepth = currentState.Cost;
                    }
                    return;
                }

                for (int moveId = 0; moveId < searchOrder.Length; moveId++)
                {

                }

                solution.Processed.Add(currentState);
            }
        }
    }
}
