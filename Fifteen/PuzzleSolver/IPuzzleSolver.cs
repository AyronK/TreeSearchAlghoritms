using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public interface IPuzzleSolver
    {
        PuzzleSolution Solve(Puzzle unsolved, Puzzle target);
    }
}
