using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            for (int i = 0; i < 4; i++)
            {
                switch (searchOrder[i].ToString())
                {
                    case "L":
                        SearchOrder[i] = Direction.Left;
                        break;
                    case "R":
                        SearchOrder[i] = Direction.Right;
                        break;
                    case "U":
                        SearchOrder[i] = Direction.Up;
                        break;
                    case "D":
                        SearchOrder[i] = Direction.Down;
                        break;
                }
            }
        }

        private const int MaxRecursionDepth = 10; 

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

        private Direction[] searchOrder = new Direction[4];

        private PuzzleSolution solution = null;

        public PuzzleSolution Solve(Puzzle unsolved, Puzzle target)
        {
            solution = new PuzzleSolution();
            DateTime startTime = DateTime.Now;

            unsolved.Cost = 0;
            solution.MaxReachedRecursionDepth = 0;
            runDFS(unsolved, target);

            solution.Duration = DateTime.Now - startTime;
            return solution;
        }

        private void runDFS(Puzzle currentState, Puzzle target)
        {
            if (currentState.Cost >= MaxRecursionDepth || solution.IsSolved)
            {
                return;
            }
            solution.Visited.Add(currentState);

            if (solution.MaxReachedRecursionDepth < currentState.Cost)
            {
                solution.MaxReachedRecursionDepth = currentState.Cost;
            }

            var possibleMoves = currentState.GetPossibleMoves();
            for (int moveId = 0; moveId < searchOrder.Length; moveId++)
            {
                if (possibleMoves.Contains(searchOrder[moveId]))
                {
                    var newState = new Puzzle(currentState.ToMatrix());
                    newState.MoveBlank(searchOrder[moveId]);
                    newState.Cost = currentState.Cost + 1;
                    newState.MovesMade = new List<Direction>(currentState.MovesMade);
                    newState.MovesMade.Add(searchOrder[moveId]);

                    if (newState.Equals(target))
                    {
                        solution.LastState = newState;
                        solution.IsSolved = true;
                        solution.Solution.Clear();
                        solution.Solution.AddRange(newState.MovesMade);
                        if (solution.MaxReachedRecursionDepth < newState.Cost)
                        {
                            solution.MaxReachedRecursionDepth = newState.Cost;
                        }
                        return;
                    }

                    if (!solution.Visited.Exists(el => el.Equals(newState)))
                    {
                        runDFS(newState, target);
                    }

                }
            }
            solution.Processed.Add(currentState);
        }
    }
}
