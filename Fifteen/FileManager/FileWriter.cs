using PuzzleSolver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class FileWriter
    {
        public static void WriteSolution(PuzzleSolution solution, string path)
        {
            using (StreamWriter solutionFile = File.AppendText(path))
            {
                if (solution.wasSolved)
                {
                    solutionFile.WriteLine(solution.Solution.Count);
                    foreach (Direction direction in solution.Solution)
                    {
                        if (direction == Direction.Up)
                        {
                            solutionFile.Write("U");
                        }
                        else if (direction == Direction.Down)
                        {
                            solutionFile.Write("D");
                        }
                        else if (direction == Direction.Left)
                        {
                            solutionFile.Write("L");
                        }
                        else if (direction == Direction.Right)
                        {
                            solutionFile.Write("R");
                        }
                    }
                }
                else
                {
                    solutionFile.Write("-1");
                }

            }
        }

        public static void WriteSolutionDetails(PuzzleSolution solution, string path)
        {
            using (StreamWriter solutionFile = File.AppendText(path))
            {
                if (solution.wasSolved)
                {
                    solutionFile.WriteLine(solution.Solution.Count);
                }
                else
                {
                    solutionFile.WriteLine("-1");
                }
                solutionFile.WriteLine(solution.VisitedCount);
                solutionFile.WriteLine(solution.ProcessedCount);
                solutionFile.WriteLine(solution.RecursionDepth);
                solutionFile.WriteLine(solution.Duration);
            }
        }
    }
}