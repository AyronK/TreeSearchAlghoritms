using FileManager;
using PuzzleSolver;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            IPuzzleSolver solver = null;
            byte[,] puzzleData = FileReader.ReadPuzzleData("");
            Puzzle puzzle = new Puzzle(puzzleData);

            PuzzleSolution solution = solver.Solve(puzzle);

            FileWriter.WriteSolution(solution.Solution, "");
            FileWriter.WriteSolutionDetails(solution, "");
        }
    }
}
