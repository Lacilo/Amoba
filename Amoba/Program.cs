using System;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;

namespace Amoba
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        // KEZDETI (TESZT) MÁTRIXOT GENERÁLÓ FÜGGVÉNY
        /// <summary>
        /// Ez a függvény egy mátrixot generál
        /// </summary>
        /// <returns></returns>
        static char[,] MatrixGenerate() 
        {
            char[,] matrix = new char[10, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    matrix[i, j] = char.Parse("x");
                }
            }

            return matrix;
        }

        // MÁTRIXOT MEGJELENÍTŐ ELJÁRÁS
        /// <summary>
        /// Ez a függvény megjeleníti a mátrixot
        /// </summary>
        /// <param name="matrix"></param>
        static void DisplayMatrix(char[,] matrix)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(matrix[i, j] + " | ");
                }

                Console.WriteLine("\n");
            }
        }
    }    
}

