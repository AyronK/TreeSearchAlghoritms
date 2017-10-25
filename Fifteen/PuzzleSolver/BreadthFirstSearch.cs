using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public class BreadthFirstSearch : IPuzzleSolver
    {
        public BreadthFirstSearch()
        {
            SearchOrder = new Direction[] { Direction.Left, Direction.Up, Direction.Right, Direction.Down };
        }

        public BreadthFirstSearch(Direction[] searchOrder)
        {
            SearchOrder = searchOrder;
        }

        public Direction[] SearchOrder {
            get
            {
                return searchOrder;
            }
            set
            {
                if (value.Length == Enum.GetValues(typeof(Direction)).Length)
                {
                    value = searchOrder;
                }
                else
                {
                    throw new ArgumentException("Wrong number of directions in order");
                }
            }
        }

        private Direction[] searchOrder;

        public PuzzleSolution Solve(Puzzle puzzle)
        {
            throw new NotImplementedException();
        }
    }
}
