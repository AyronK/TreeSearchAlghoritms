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
            Puzzle target = new Puzzle(new byte[,]{ { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 8, 10, 11, 12 }, { 13, 14, 15, 0 } });
            PuzzleSolution solution = solver.Solve(puzzle, target);

            //FileWriter.WriteSolution(solution.Solution, "");
            FileWriter.WriteSolutionDetails(solution, "");
        }
    }
}
