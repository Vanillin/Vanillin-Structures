using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DecodeShifrVizhener
{
    internal class Program
    {
        static bool boolPrintText = false;
        static bool boolPrintDoubles = false;
        static Decoder decoder;

        static int ShowMenu(string[] menu, ref int vybor)
        {
            int lengthMenu = menu.Length;
            ConsoleKeyInfo cki;
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < lengthMenu; i++)
                {
                    if (vybor == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(menu[i]);
                }
                Console.ForegroundColor = ConsoleColor.White;
                if (decoder != null && decoder.MemoryPartText != null)
                {
                    if (boolPrintText)
                        PrintText();
                    if (boolPrintDoubles)
                        AnalyzeVerSymbols();
                }
                Console.WriteLine();

                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    vybor--;
                    if (vybor < 0) { vybor = lengthMenu - 1; }
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    vybor++;
                    if (vybor > lengthMenu - 1) { vybor = 0; }
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    return vybor + 1;
                }
            }
        }
        static void PrintText()
        {
            bool End = false;
            int i = 0;
            int count = 0;
            while (!End)
            {
                foreach (var v in decoder.MemoryPartText)
                {
                    if (v.Count <= i) { End = true; break; }
                    Console.Write(v[i]);
                }
                Console.Write("-");
                count++;
                if (count == 7)
                {
                    count = 0;
                    Console.WriteLine();
                }
                i++;
            }
        }
        static int ReadConsoleInt(string message)
        {
            Console.WriteLine(message);
            string str = Console.ReadLine();
            while (str == "" || str == null) str = Console.ReadLine();
            return int.Parse(str);
        }
        static string ReadConsoleString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        static void AnalyzeVerSymbols()
        {
            var lists = decoder.AnalyzeVerSymbols();
            foreach (var list in lists)
            {
                Console.WriteLine("---------");
                int count = 0;
                foreach ( var doub in list)
                {
                    double print = doub.Item2;
                    if (print < 0.015)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (print > 0.065)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    count++;
                    if (count > 7) { Console.WriteLine(); count = 0; }
                    Console.Write($"{doub.Item1} = {print}, ");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            string[] menu = { "1. Прочитать текст и разделить его на N частей",
                "2. Вывести/убрать текст на экран",
                "3. Вывести/убрать вероятности букв в тексте",
                "4. Автоматическая подгонка вероятностей",
                "5. Сместить вериятности всех букв 1 раз влево",
                "6. Сместить вероятности в строке K вправо N раз",
                "7. Сместить вероятности в строке K влево N раз",
                "8. Получить расшифрованный текст (при условии, что текст оригинальный)",
                "9. Выход"
            };

            int position = 0;
            int p = ShowMenu(menu, ref position);

            while (p < menu.Length)
            {
                switch (p)
                {
                    case 1:
                        {
                            string path = ReadConsoleString("Введите полный путь к файлу");                            
                            int N = ReadConsoleInt("Введите N");
                            using (StreamReader sr = new StreamReader(path)) 
                            {
                                decoder = new Decoder(sr.ReadToEnd(), N);
                            }
                            break;
                        }
                    case 2:
                        {
                            boolPrintText = !boolPrintText;
                            boolPrintDoubles = false;
                            break;
                        }
                    case 3:
                        {
                            boolPrintDoubles = !boolPrintDoubles;
                            boolPrintText = false;
                            break;
                        }
                    case 4:
                        {
                            if (decoder == null) Console.WriteLine("Текст не прочитан");
                            else decoder.AutomaticalyShift();
                            break;
                        }
                    case 5:
                        {
                            if (decoder == null) Console.WriteLine("Текст не прочитан");
                            else decoder.ChangeAllShiftOnMas();
                            break;
                        }
                    case 6:
                        {
                            if (decoder == null) Console.WriteLine("Текст не прочитан");
                            else decoder.ChangeShiftOnMas(ReadConsoleInt("K"), ReadConsoleInt("N"));                            
                            break;

                        }
                    case 7:
                        {
                            if (decoder == null) Console.WriteLine("Текст не прочитан");
                            else decoder.ChangeShiftOnMas(ReadConsoleInt("K"), -ReadConsoleInt("N"));
                            break;
                        }
                    case 8:
                        {
                            if (decoder == null) Console.WriteLine("Текст не прочитан");
                            else
                            {
                                decoder.ChangeShiftOnMas(6, -8);
                                decoder.ChangeShiftOnMas(7, -13);
                                decoder.ChangeShiftOnMas(8, -8);
                                decoder.ChangeShiftOnMas(9, 3);
                                decoder.ChangeShiftOnMas(0, -10);
                                decoder.ChangeShiftOnMas(1, 13);
                                decoder.ChangeShiftOnMas(2, 16);
                                decoder.ChangeShiftOnMas(3, -12);
                                decoder.ChangeShiftOnMas(4, -9);
                                decoder.ChangeShiftOnMas(5, 4);
                            }
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
                Console.WriteLine("Нажмите любую кнопку");
                Console.ReadKey();
                p = ShowMenu(menu, ref position);
            }
        }        
    }
}

