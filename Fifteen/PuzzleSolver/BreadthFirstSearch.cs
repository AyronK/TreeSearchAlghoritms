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
            searchOrder = new Direction[] { Direction.Left, Direction.Up, Direction.Right, Direction.Down };
        }

        public BreadthFirstSearch(Direction[] searchOrder)
        {
            SearchOrder = searchOrder;
        }

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

        public PuzzleSolution Solve(Puzzle puzzle)
        {
            throw new NotImplementedException();
        }
    }
}
