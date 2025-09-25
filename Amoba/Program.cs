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
                        break;


                }
            }
            while (aktualisPont != 1);
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
    }
}

