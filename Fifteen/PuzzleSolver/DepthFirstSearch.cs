using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public class DepthFirstSearch : IPuzzleSolver
    {
        public DepthFirstSearch()
        {
            searchOrder = new Direction[] { Direction.Left, Direction.Up, Direction.Right, Direction.Down };
        }

        public DepthFirstSearch(Direction[] searchOrder)
        {
            SearchOrder = searchOrder;
        }

        public DepthFirstSearch(string searchOrder)
        {
            for( int i = 0; i < 4; i++ )
            {
                switch(searchOrder[i].ToString())
                {
                    case "L":
                        SearchOrder[i] = Direction.Left;
                        break;
                    case "R":
                        SearchOrder[i] = Direction.Right;
                        break;
                    case "U":
                        SearchOrder[i] = Direction. Up;
                        break;
                    case "D":
                        SearchOrder[i] = Direction.Down;
                        break;
                }
            }
        }

        public int MaxRecursionDepth = 20;

        public Direction[] SearchOrder
        {
            get
            {
                return searchOrder;
            }
            set
            {
                if (value.Length == Enum.GetValues(typeof(Direction)).Length)
                {
                    searchOrder = value;
                }
                else
                {
                    throw new ArgumentException("Wrong number of directions in order");
                }
            }
        }

        private Direction[] searchOrder = null;

        private PuzzleSolution solution = null;

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
                var currentState = queue.Dequeue();
                //runDFS(currentState, target);

                solution.Visited.Add(currentState);
                
                var possibleMoves = currentState.GetPossibleMoves();

                for (int moveId = 0; moveId < searchOrder.Length; moveId++)
                {
                    if (possibleMoves.Contains(searchOrder[moveId]))
                    {
                        var newState = new Puzzle(currentState.ToMatrix());
                        newState.MoveBlank(searchOrder[moveId]);

                        if (newState.Equals(target))
                        {
                            solution.LastState = newState;
                            solution.IsSolved = true;
                            solution.Solution.Add(searchOrder[moveId]);
                            return;
                        }

                        solution.Solution.Add(searchOrder[moveId]);
                        runDFS(newState, target);

                        if(solution.IsSolved)
                        {
                            return;
                        }
                    }
                }

            solution.Processed.Add(currentState);
            }
        }

        private void runDFS(Puzzle currentState, Puzzle target)
        {
            solution.Visited.Add(currentState);
            solution.RecursionDepth++;

            var possibleMoves = currentState.GetPossibleMoves();
            for (int moveId = 0; moveId < searchOrder.Length; moveId++)
            {
                if (possibleMoves.Contains(searchOrder[moveId]))
                {
                    if (solution.RecursionDepth == MaxRecursionDepth)
                    {
                        solution.IsSolved = false;
                        solution.RecursionDepth--;
                        return;
                    }

                    var newState = new Puzzle(currentState.ToMatrix());
                    newState.MoveBlank(searchOrder[moveId]);

                    if (newState.Equals(target))
                    {
                        solution.LastState = newState;
                        solution.IsSolved = true;
                        solution.Solution.Add(searchOrder[moveId]);
                        return;
                    }

                    if (solution.Visited.Find(el => el.Equals(newState)) == null)
                    {
                        solution.Solution.Add(searchOrder[moveId]);
                        runDFS(newState, target);
                    }

                }
            }
            solution.Processed.Add(currentState);

        }
    }
}
