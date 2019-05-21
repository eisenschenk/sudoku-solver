using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver

{
    public class ninth
    {
        public List<int> NinthResult;

        public ninth()
        {
            NinthResult = new List<int>();
        }
    }
    public class tile
    {

        public int ResultValue;
        public List<int> PotentialValue;
        public int GetRow;
        public int GetCul;
        public int GetNinth;

        public tile(int index, int resultValue)
        {
            //fill list
            ResultValue = resultValue;
            PotentialValue = new List<int>();
            GetRow = index / 9;
            GetCul = index % 9;
            GetNinth = GetRow / 3 * 3 + GetCul / 3;

            if (ResultValue != 0)
                PotentialValue.Add(ResultValue);
            else
                for (int i = 0; i < 9; i++)
                {
                    PotentialValue.Add(i + 1);
                }
        }
    }

    class Program
    {
        //write in Ninth list
        static void FillNinth(tile[] array, ninth[] smallarray, int index)
        {
            if (array[index].ResultValue != 0)
            {
                smallarray[array[index].GetNinth].NinthResult.Add(array[index].ResultValue);
            }
        }
        //remove results from ninth list in potentialvalues list
        static void RemoveNinthResult(tile[] array, ninth[] smallArray, int index)
        {
            foreach (int item in smallArray[array[index].GetNinth].NinthResult)
            {
                array[index].PotentialValue.Remove(item);
            }
        }
        //remove results from other tiles in same column / row
        static void RemovePotentialValues(tile[] array, ninth[] smallArray, int index)
        {
            if (array[index].ResultValue == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    array[index].PotentialValue.Remove(array[array[index].GetRow * 9 + i].ResultValue);
                    array[index].PotentialValue.Remove(array[i * 9 + array[index].GetCul].ResultValue);                    
                }
            }
        }
        //write potentialvalues into result if count == 1
        static void SetResults(tile[] array, int index)
        {
            if (array[index].PotentialValue.Count == 1)
            {
                foreach (int item in array[index].PotentialValue)
                {
                    array[index].ResultValue = item;
                }
            }
        }        
        //add unsolved tiles to unsolved list
        static void AddUnSolved(tile[] array, int index, List<int> Unsolved)
        {
            if (array[index].ResultValue == 0)
            {
                Unsolved.Add(index);
            }
        }
        //remove solved tiles from unsolved list
        static void RemoveUnSolved(tile[] array, int index, List<int> Unsolved)
        {
            if (array[index].ResultValue != 0)
            {
                Unsolved.Remove(index);
            }
        }
        static void SudokuDraw(tile[] array, int row, int cul)
        {
            if ((row == 2 || row == 5) && cul == 8)
            {
                Console.WriteLine(array[row*9 + cul].ResultValue.ToString("0"));
                Console.Write("-------------------");
            }
            else
            {
                if (cul == 2 || cul == 5)
                {
                    Console.Write(array[row * 9 + cul].ResultValue.ToString("0"));
                    Console.Write("|");
                }

                else
                {
                    Console.Write(array[row * 9 + cul].ResultValue.ToString("0"));
                }
            }

            Console.Write(" ");
        }

        static void Main(string[] args)
        {
            tile[] array = new tile[81];
            ninth[] smallArray = new ninth[9];
            List<int> Unsolved = new List<int>();

            int[] sudoku1 = new int[] {
                     0,0,0,  1,0,0,  0,0,0,
                     2,0,0,  0,7,0,  0,3,0,
                     0,0,6,  0,8,3,  0,9,0,
                     0,0,7,  4,0,1,  3,0,0,
                     3,5,8,  0,6,0,  1,0,0,
                     0,1,0,  9,0,0,  0,0,0,
                     9,0,0,  6,0,0,  5,0,0,
                     0,4,0,  0,0,0,  0,0,7,
                     0,0,3,  0,0,5,  0,0,0};

            //tile array erstellen
            for (int i = 0; i < 81; i++)
            {
                array[i] = new tile(i, sudoku1[i]);
                AddUnSolved(array, i, Unsolved);
            }
            //ninth array erstellen
            for (int i = 0; i < 9; i++)
            {
                smallArray[i] = new ninth();
            }           
            //loop to calculate sudoku
            while (Unsolved.Count > 0)                       
                for (int i = 0; i < 81; i++)
                {
                    FillNinth(array, smallArray, i);
                    RemoveNinthResult(array, smallArray, i);
                    RemovePotentialValues(array, smallArray, i);
                    SetResults(array, i);                    
                    RemoveUnSolved(array, i, Unsolved);
                }           
            //output result

            for (int row = 0; row < 9; row++)
            {
                Console.WriteLine();
                for (int cul = 0; cul < 9; cul++)
                    SudokuDraw(array, row, cul);
            }          
            Console.ReadKey();
        }

    }
}
