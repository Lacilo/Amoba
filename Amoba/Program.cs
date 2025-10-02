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
            int mSize = 10;
            int[] pos = { 0, 0 };
            string[,] matrix = MatrixGenerate(mSize);
            string uInput = "";
            int[] uPos = new int[2];
            var uKey;
            int[] vPos = new int[2];

            Menu();
            DisplayMatrix(matrix, pos);

            do
            {
                Console.Write("Adja meg a pozíciót --> ");
                uInput = Console.ReadLine();

                uKey = Console.ReadKey();

                if (uInput != "") { 
                    uPos[0] = int.Parse(uInput.Split(' ')[0]);
                    uPos[1] = int.Parse(uInput.Split(' ')[1]);
                if (uInput != "")
                {
                    uPos[0] = int.Parse(uInput.Split(' ')[0]) - 1;
                    uPos[1] = int.Parse(uInput.Split(' ')[1]) - 1;

                    matrix = MatrixAppend(matrix, uPos[0], uPos[1], "x");
                }
                else
                {
                    do
                    {
                        pos = ChangePos(pos, mSize);

                        if (pos[0] != -1)
                        {
                            vPos[0] = pos[0];
                            vPos[1] = pos[1];
                        }

                        DisplayMatrix(matrix, vPos);
                    }
                    while (pos[0] != -1);

                    Console.WriteLine($"{vPos[0]} {vPos[1]}");
                    matrix = MatrixAppend(matrix, vPos[0], vPos[1], "x");
                    
                }

                DisplayMatrix(matrix, vPos);

                pos[0] = vPos[0];
                pos[1] = vPos[1];
            }
            while (true);
        }

        private static void Menu()
        {
            int aktualisPont = 0;
            do
            {
                bool select = false;
                do
                {
                    ShowMenu(aktualisPont);
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Enter:
                            select = true;
                            break;
                        case ConsoleKey.UpArrow:
                            if (aktualisPont > 0)
                            {
                                aktualisPont--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (aktualisPont < 1)
                            {
                                aktualisPont++;
                            }
                            break;
                        default:
                            Console.Beep();
                            break;
                    }

                }
                while (!select);
                switch (aktualisPont)
                {
                    case 0: //Új játék

                        break;
                    case 1: //Kilépés
                        Console.Clear();
                        Console.SetCursorPosition(50, 0);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Amőba 1.0 Menu");
                        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                        Console.Write("Biztosan kilépsz?");
                        if (Console.ReadKey().Key != ConsoleKey.Enter)
                        {
                            aktualisPont = 0;
                        }
                        Environment.Exit(0);
                        break;


                }
            }
            while (aktualisPont != 0);
        }

        static int[] ChangePos(int[] pos, int mSize)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    pos[1]--;

                    break;

                case ConsoleKey.RightArrow:
                    pos[0]++;

                    break;

                case ConsoleKey.LeftArrow:
                    pos[0]--;
                    break;

                case ConsoleKey.DownArrow:
                    pos[1]++;                  
                    break;

                case ConsoleKey.Enter:
                    pos[0] = -1;
                    pos[1] = -1;
                    break;

                default:
                    break;
            }

            return pos;
        }

        static string[,] MatrixAppend(string[,] matrix, int pos_y, int pos_x, string character)
        {
            // ITT HÍVNÁM MEG A FÜGGVÉNYT AMI ELLENŐRZI HOGY JÓ HELYRE TESZI-E ENNEK EGY BOOLEANT KELL VISSZAADNIA
            bool correctPlace = true; // FüggvényedNeve(pos_x, pos_y);

            if (correctPlace)
            {
                matrix[pos_x, pos_y] = character;
            }
            else
            {

            }

            return matrix;
        }

        static void ShowMenu(int cPoint)
        {
            Console.Clear();
            Console.SetCursorPosition(50, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Amőba 1.0 Menu");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

            if (cPoint == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("1 - Új játék");
            if (cPoint == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("2 - Kilépés");
            if (cPoint == 2)
            {
                Console.ForegroundColor = ConsoleColor.Green;

            }
        }


        // KEZDETI (TESZT) MÁTRIXOT GENERÁLÓ FÜGGVÉNY
        /// <summary>
        /// Ez a függvény egy mátrixot generál
        /// </summary>
        /// <returns></returns>
        static string[,] MatrixGenerate(int mSize = 10)
        {
            string[,] matrix = new string[mSize, mSize];

            for (int i = 0; i < mSize; i++)
            {
                for (int j = 0; j < mSize; j++)
                {
                    matrix[i, j] = " ";
                }
            }

            return matrix;
        }

        // MÁTRIXOT MEGJELENÍTŐ ELJÁRÁS
        /// <summary>
        /// Ez a függvény megjeleníti a mátrixot
        /// </summary>
        /// <param name="matrix"></param>
        static void DisplayMatrix(object[,] matrix, int[] pos)
        {
            Console.Clear();

            int mDSize = matrix.GetLength(0);

            // FELSŐ SÁV KIRAJZOLÁSA
            Console.Write("┌─");

            for (int i = 0; i < mDSize - 1; i++)
            {
                Console.Write("──┬─");
            }

            Console.Write("──┐\n");


            // MÁTRIX KIRAJZOLÁSA
            for (int i = 0; i < mDSize; i++)
            {
                Console.Write("│ ");
                for (int j = 0; j < mDSize; j++)
                {
                    if (i == pos[1] && j == pos[0])
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write(matrix[i, j]);

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(" │ ");
                }

                // HA MÉG NEM AZ UTOLSÓ SORNÁL JÁR
                if (i != mDSize - 1)
                {
                    Console.Write("\n├─");

                    for (int x = 0; x < mDSize - 1; x++)
                    {
                        Console.Write("──┼─");
                    }

                    Console.WriteLine("──┤");
                }
            }


            // ALSÓ SÁV KIRAJZOLÁSA
            Console.Write("\n└─");

            for (int i = 0; i < mDSize - 1; i++)
            {
                Console.Write("──┴─");
            }

            Console.Write("──┘\n");
        }

    }
}