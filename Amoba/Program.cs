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
using System.Threading.Tasks.Sources;

namespace Amoba
{
    // FŐ PROGRAM
    internal class Program
    {
        static void Main(string[] args)
        {
            int mSize = 10;
            int[] pos = { 0, 0 };            
            string uInput = "";
            int[] uPos = new int[2];
            int[] vPos = new int[2];
            string symbol = "x";
            bool validPos = true;
            string mInput = "";


            string[,] matrix = MatrixGenerate(mSize);

            Menu();
            Console.Clear();
            Console.Write("Adja meg a játéktér méretét (min 5, pl.: \\-$ 12), az alapértelmezett 10x10-es mérethez hagyja üresen \\-$ ");
            mInput = Console.ReadLine();

            if (mInput != "" && int.Parse(mInput) > 5)
            {
                mSize = int.Parse(mInput);
            }

            DisplayMatrix(matrix, pos, false);

            do
            {
                try
                {
                    Console.Write("Adja meg a pozíciót --> ");
                    uInput = Console.ReadLine();

                    if (uInput != "")
                    {
                        uPos[0] = int.Parse(uInput.Split(' ')[0]) - 1;
                        uPos[1] = int.Parse(uInput.Split(' ')[1]) - 1;

                        validPos = PositionCheck(matrix, uPos[0], uPos[1]);

                        if (validPos)
                        {
                            matrix = MatrixAppend(matrix, uPos[1], uPos[0], symbol);
                            symbol = ChangeSymbol(symbol, validPos);
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        DisplayMatrix(matrix, vPos, true);

                        do
                        {
                            pos = ChangePos(pos, mSize);

                            if (pos[0] != -1)
                            {
                                vPos[0] = pos[0];
                                vPos[1] = pos[1];
                            }

                            DisplayMatrix(matrix, vPos, true);
                        }
                        while (pos[0] != -1);

                        validPos = PositionCheck(matrix, vPos[1], vPos[0]);

                        if (validPos)
                        {
                            matrix = MatrixAppend(matrix, vPos[0], vPos[1], symbol);

                            symbol = ChangeSymbol(symbol, validPos);
                        }
                        else
                        {

                        }
                    }

                    DisplayMatrix(matrix, vPos, false);

                    pos[0] = vPos[0];
                    pos[1] = vPos[1];

                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Érvénytelen pozíció!");
                }
                catch (Exception) {
                    Console.WriteLine("Helytelen input!");
                }

            }
            while (!(HorizontalCheck(matrix, pos[0], pos[1]) || VerticalCheck(matrix, pos[0], pos[1]) ||DiagonalRightCheck(matrix, pos[0], pos[1]) ||DiagonalLeftCheck(matrix, pos[0], pos[1])));
            

            static string ChangeSymbol(string symbol, bool validPos)
            {
                if (symbol == "x" && validPos)
                {
                    symbol = "o";
                }
                else
                {
                    symbol = "x";
                }

                return symbol;
            }
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

        static bool PositionCheck(string[,] matrix, int x, int y)
        {
            if (matrix[x, y] == " ")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool HorizontalCheck(string[,] matrix, int x, int y)
        {
            string row = "";
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {

                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    row += matrix[i, j];
                }
            }

            return WinnerCheck(row);

        }

        private static bool WinnerCheck(string row)
        {
            if (row.Contains("xxxxx"))
            {
                Console.WriteLine("Az X nyert");
                return true;
            }
            else if (row.Contains("ooooo"))
            {
                Console.WriteLine("A O nyert");
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool VerticalCheck(string[,] matrix, int x, int y)
        {
            string column = "";
            for (int i = 0; i < matrix.GetLength(1) - 1; i++)
            {

                for (int j = 0; j < matrix.GetLength(0) - 1; j++)
                {
                    column += matrix[j, i];
                }
            }

            return WinnerCheck(column);            
        }


        static bool DiagonalRightCheck(string[,] matrix, int x, int y) 
        {
            string diagonalRight = "";
            for (int i = 4; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 5; j++)
                {
                    // ÁTLÓS ELLENŐRZÉSE JOBBRA
                    diagonalRight += matrix[i, j] + matrix[i-1, j+1] + matrix[i-2, j+2] + matrix[i-3, j+3] + matrix[i-4, j+4];
                    
                    if (diagonalRight.Contains("xxxxx") || diagonalRight.Contains("ooooo"))
                    {
                        if (diagonalRight.Contains("xxxxx"))
                        {
                            Console.WriteLine("Az X nyert");
                        }
                        else
                        {
                            Console.WriteLine("A O nyert");
                        }
                        return true;
                    }
                }
                diagonalRight = "";
            }
            return false;
        }

        static bool DiagonalLeftCheck(string[,] matrix, int x, int y)
        {
            string diagonalLeft = "";
            for (int i = 0; i < matrix.GetLength(0)-5; i++)
            {
                for (int j = 0; j < matrix.GetLength(1)-5; j++)
                {
                    // ÁTLÓS ELLENŐRZÉSE BALRA
                    diagonalLeft += matrix[i, j] + matrix[i + 1, j + 1] + matrix[i + 2, j + 2] + matrix[i + 3, j + 3] + matrix[i + 4, j + 4];

                    if (diagonalLeft.Contains("xxxxx") || diagonalLeft.Contains("ooooo"))
                    {
                        if (diagonalLeft.Contains("xxxxx"))
                        {
                            Console.WriteLine("Az X nyert");
                        }
                        else
                        {
                            Console.WriteLine("A O nyert");
                        }
                        return true;
                    }
                }
                diagonalLeft = "";
            }
            return false;
        }

        static int[] ChangePos(int[] pos, int mSize)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    if (pos[1] - 1 >= 0)
                    {
                        pos[1]--;
                    }                   

                    break;

                case ConsoleKey.RightArrow:
                    if (pos[0] + 1 < mSize)
                    {
                        pos[0]++;
                    }                    

                    break;

                case ConsoleKey.LeftArrow:
                    if (pos[0] - 1 >= 0)
                    {
                        pos[0]--;
                    }                    
                    
                    break;

                case ConsoleKey.DownArrow:
                    if (pos[1] + 1 < mSize)
                    {
                        pos[1]++;
                    }                    

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
        static void DisplayMatrix(object[,] matrix, int[] pos, bool cursorDisplayed)
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
                    if (matrix[i, j] == "x")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (i == pos[1] && j == pos[0] && cursorDisplayed)
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
