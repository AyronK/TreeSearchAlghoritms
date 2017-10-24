using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public class PuzzleSolution
    {
        public Direction[] Solution { get; private set; }
        public int SeenCount => Seen.Count;
        public int ProcessedCount => Processed.Count;
        public int MaxRecursionDepth { get; private set; }
        public DateTime Duration { get; private set; }

        private List<Puzzle> Seen { get; set; }
        private List<Puzzle> Processed { get; set; }
    }
}
