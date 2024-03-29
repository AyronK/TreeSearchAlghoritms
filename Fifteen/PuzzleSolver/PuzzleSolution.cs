﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    public class PuzzleSolution
    {
        public List<Direction> Solution { get; private set; } = new List<Direction>();
        public int VisitedCount => Visited.Count;
        public int ProcessedCount => Processed.Count;
        public int RecursionDepth { get; internal set; }
        public int MaxReachedRecursionDepth { get; internal set; }
        public int MovesMade { get; internal set; }
        public TimeSpan Duration { get; internal set; } = TimeSpan.Zero;
        public List<Puzzle> Visited { get; set; } = new List<Puzzle>();
        public List<Puzzle> Processed { get; set; } = new List<Puzzle>();

        public Puzzle LastState { get; set; }

        public bool IsSolved { get; set; } = false;
    }
}
