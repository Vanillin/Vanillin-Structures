using System;
using System.Collections.Generic;

namespace MyEncryption
{
    internal class LinCode5_2 : IShift
    {
        public LinCode5_2() { }

        int[,] MatrixG = new int[2, 5] //любые два вектора (независимых)
        {
            { 1, 1, 0, 1, 0 },
            { 0, 1, 1, 0, 1 }
        };
        int[,] MatrixH = new int[3, 5] //матрица H, любые три вектора (независимых) //высчитывается из G
        {
            { 1, 1, 1, 0, 0 },
            { 1, 0, 0, 1, 0 },
            { 0, 0, 1, 0, 1 }
        };
        Dictionary<string, int> Syndromes = new Dictionary<string, int>
        {
            { "000", 0 },
            { "110", 1 },
            { "100", 2 },
            { "101", 3 },
            { "010", 4 },
            { "001", 5 },
            { "111", -1 },
            { "011", -1 }
        };
        public int[] MethodCodir(int a, int b)
        {
            int[] code = new int[5]; //закодированное слово
            for (int i = 0; i < 5; i++)
            {
                code[i] = (a * MatrixG[0, i] + b * MatrixG[1, i]) % 2;
            }
            return code;
        }
        public int[] FixCode(int[] code)
        {
            int[] curSindrom = new int[3]; //высчитывает синдром
            for (int j = 0; j < 5; j++)
            {
                if (code[j] == 1)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        curSindrom[k] += MatrixH[k, j];
                        if (curSindrom[k] == 2) 
                            curSindrom[k] = 0;
                    }
                }
            }

            string sindrom = $"{curSindrom[0]}{curSindrom[1]}{curSindrom[2]}";
            int indexCurrent = Syndromes[sindrom];
            if (indexCurrent == 0) return code; //ошибки нет
            if (indexCurrent < 0) throw new Exception("Слишком много ошибок, нельзя восстановить");

            // Заменяем символ в указанной позиции
            if (code[indexCurrent-1] == 1)
            {
                code[indexCurrent-1] = 0;
            }
            else
            {
                code[indexCurrent-1] = 1;
            }
            return code;
        }

        public void Codirov(string Text, out string zacodirText, out Dictionary<string, string> Diction, out int WeightZacodirText)
        {
            string[] ints = Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] result = MethodCodir(int.Parse(ints[0]), int.Parse(ints[1]));
            zacodirText = "";
            foreach (var v in result)
            {
                zacodirText += $"{v} ";
            }
            Diction = new Dictionary<string, string>();
            WeightZacodirText = result.Length;
        }

        public void Decodirov(string zacodirText, Dictionary<string, string> Diction, out string Text)
        {
            string[] ints = zacodirText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] ints1 = new int[ints.Length];
            for (int i = 0; i < ints.Length; i++)
                ints1[i] = int.Parse(ints[i]);

            int[] result = FixCode(ints1);
            Text = "";
            foreach (var v in result)
            {
                Text += $"{v} ";
            }
        }
    }
}