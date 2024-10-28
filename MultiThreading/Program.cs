using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    static class Program
    {
        static Queue<string> Paths = new Queue<string>();
        static ReaderWriterLock RWLock = new ReaderWriterLock();
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь папки, файлы которой будут анализироваться: ");
            string PathFile = Console.ReadLine();
            bool ReadNow = true;

            #region TaskCalculate
            Task CalculateFile = Task.Run(() =>
            {
                while (Paths.Count != 0 || ReadNow)
                {
                    if (Paths.Count != 0)
                    {
                        RWLock.AcquireWriterLock(100);    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        FileInfo f = new FileInfo(Paths.Dequeue());
                        Console.WriteLine($"  Анализ файла: {f.FullName}");
                        RWLock.ReleaseWriterLock();
                        MyByte Sum = new MyByte(0);
                        byte[] ar = File.ReadAllBytes(f.FullName);
                        foreach (byte a in ar)
                        {
                            if (a % 2 != 0)
                            {
                                Sum += new MyByte(a);
                            }
                        }
                        Console.WriteLine($"  Анализ файла пройден: {Sum.ToString()} <- {f.FullName}");
                    }
                }
            });
            #endregion
            #region TaskRead
            Task ReadFile = Task.Run(() =>
            {
                Queue<string> qu = new Queue<string>();
                qu.Enqueue(PathFile);
                while (qu.Count != 0)
                {
                    string folder = qu.Dequeue();
                    foreach (string str in Directory.GetDirectories(folder))
                    {
                        qu.Enqueue(str);
                    }
                    foreach (string str in Directory.GetFiles(folder))
                    {
                        if (File.Exists(str))
                        {
                            RWLock.AcquireReaderLock(100);    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            Paths.Enqueue(new FileInfo(str).FullName);
                            Console.WriteLine($"  Файл добавлен в очередь: {new FileInfo(str).FullName}");
                            RWLock.ReleaseReaderLock();
                        }
                    }
                }
                ReadNow = false;
            });
            #endregion
            Console.ReadKey();
        }
    }
}
