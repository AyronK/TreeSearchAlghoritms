using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class FileReader
    {
        public static byte[,] ReadPuzzleData(string fileName)
        {
            try
            {
                string puzzleData = null;
                //string path = Directory.GetCurrentDirectory().ToString() + fileName;
                string path = fileName;
                using (StreamReader reader = new StreamReader(path))
                {
                    var s = reader.ReadToEnd();
                    puzzleData = s.Replace(" ", ",").Replace("\n", "").Replace("\r", ",").Replace("\t", ",");
                }

                var x = puzzleData.Split(',');

                int rowsNumber = int.Parse(x[0].ToString());
                int columnsNumber = int.Parse(x[1].ToString());
                byte[,] puzzle = new byte[rowsNumber, columnsNumber];

                for(int i = 0; i < rowsNumber; i++)
                {
                    for (int j = 0; j < columnsNumber; j++)
                    {
                        puzzle[i,j] = byte.Parse(x[i * columnsNumber + j + 2].ToString());
                    }
                }

                return puzzle;

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
