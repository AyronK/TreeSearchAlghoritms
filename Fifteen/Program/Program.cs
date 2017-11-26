using FileManager;
using PuzzleSolver;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            IPuzzleSolver solver = null;

            string strategy = null;
            string details = null;
            string enteringPuzzleFile = null;
            string solutionPuzzleFile = null;
            string additionalInformationFile = null;

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
                enteringPuzzleFile = args[2];
                solutionPuzzleFile = args[3];
                additionalInformationFile = args[4];

                switch(strategy)
                {
                    case "bfs":
                        solver = new BreadthFirstSearch(details);
                        break;
                    //case "dfs":
                    //    solver = new DepthFirstSearch();
                    //    break;
                    case "astr":
                        solver = new AStar();
                        ((AStar)solver).heuristicType = details;
                        break;
                }

                byte[,] puzzleData = FileReader.ReadPuzzleData(enteringPuzzleFile);
                Puzzle puzzle = new Puzzle(puzzleData);
                Puzzle target = new Puzzle(new byte[,]{ { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0 } });
                PuzzleSolution solution = solver.Solve(puzzle, target);
                FileWriter.WriteSolution(solution, solutionPuzzleFile);
                FileWriter.WriteSolutionDetails(solution, additionalInformationFile);
                               
            }
        }
    }
}
