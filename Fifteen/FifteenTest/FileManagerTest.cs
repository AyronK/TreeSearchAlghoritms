using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuzzleSolver;
using FileManager;
using System.Linq;
using System.IO;

namespace FifteenTest
{
    [TestClass]
    public class FileManagerTest
    {
        [TestMethod]
        public void ReadPuzzleFromFileTest()
        {
            byte[,] testTab = { { 0, 1 }, { 2, 3 } };
            Puzzle newPuzzle = new Puzzle(testTab);
            

          //  byte[,] tabFromFile = FileReader.ReadPuzzleData("C:\\Users\\Ayron\\Desktop\\testPuzzle.txt");
          //  Puzzle puzzleFromFile = new Puzzle(tabFromFile);

            //Console.WriteLine(tabFromFile.ToString());

            for (int i = 0; i < newPuzzle.RowsCount; i++)
            {
                for (int j = 0; j < newPuzzle.ColumnsCount; j++)
                {
              //      Assert.AreEqual(testTab[i, j], puzzleFromFile[i, j]);
                }
            }

         
        }
    }
}
