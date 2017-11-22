using FileManager;
using PuzzleSolver;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            IPuzzleSolver solver = null;
            //byte[,] puzzleData = FileReader.ReadPuzzleData("");
            //Puzzle puzzle = new Puzzle(puzzleData);
            //Puzzle target = new Puzzle(new byte[,]{ { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 8, 10, 11, 12 }, { 13, 14, 15, 0 } });
            //PuzzleSolution solution = solver.Solve(puzzle, target);

            ////FileWriter.WriteSolution(solution.Solution, "");
            //FileWriter.WriteSolutionDetails(solution, "");

            string strategy = null;
            string details = null;
            string enteringPuzzle = null;
            string solutionPuzzle = null;
            string additionalInformation = null;

            if (args.Length == 0)
            {
                System.Console.WriteLine("Arguments not entered");
            }
            else if (args.Length != 5)
            {
                System.Console.WriteLine("Wrong number of arguments"); 
            }
            else
            {
                strategy = args[0];
                details = args[1];
                enteringPuzzle = args[2];
                solutionPuzzle = args[3];
                additionalInformation = args[5];

           
                
            }
        }
    }
}
