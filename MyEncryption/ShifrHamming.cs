using System;
using System.Collections.Generic;
using System.Text;

namespace MyEncryption
{
    public class ShifrHamming : IShift
    {
        public void Codirov(string Text, out string zacodirText, out Dictionary<string, string> Diction, out int WeightZacodirText)
        {
            Diction = new Dictionary<string, string>();
            byte[] bytes = Encoding.UTF8.GetBytes(Text);
            StringBuilder line = new StringBuilder();
            zacodirText = "";
            foreach (var i in bytes)
            {
                line.Append(Convert.ToString(i, 2).PadLeft(8, '0'));
            }

            int pow = 0;
            for (int i = 0; i < Convert.ToInt32(Math.Ceiling(Math.Log(line.Length - i + 1, 2))); i++)
            {
                line.Insert(Convert.ToInt32(Math.Pow(2, pow)) - 1, "0");
                pow++;
            }
            pow = 0;
            for (int i = 1; i <= line.Length; i++)
            {
                if (i == Convert.ToInt32(Math.Pow(2, pow)))
                {
                    int sum = 0;
                    for (int j = i; j <= line.Length; j += (i * 2))
                    {
                        for (int k = j; k < j + i; k++)
                        {
                            if (k > line.Length) break;
                            sum += line[k - 1];
                        }
                    }
                    zacodirText += (sum % 2);
                    pow++;
                }
                else
                {
                    zacodirText += line[i - 1];
                }

            }
            WeightZacodirText = zacodirText.Length;
        }

        public void Decodirov(string zacodirText, Dictionary<string, string> Diction, out string Text)
        {
            int misstake = ChangeMisstake(zacodirText);
            StringBuilder answer = new StringBuilder();
            int pow = 0;
            for (int i = 1; i <= zacodirText.Length; i++)
            {
                if (i == Convert.ToInt32(Math.Pow(2, pow)))
                {
                    pow++;
                    continue;
                }
                if (i == misstake)
                {
                    answer.Append((int.Parse(zacodirText[i - 1].ToString()) + 1) % 2);
                    continue;
                }
                answer.Append(zacodirText[i - 1]);
            }
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < answer.Length; i += 8) bytes.Add(Convert.ToByte(answer.ToString().Substring(i, 8), 2));
            Text = Encoding.UTF8.GetString(bytes.ToArray());
            if (misstake >= 0)
            {
                Text = String.Format($"{Text}\n\r Найдена и исправлена ошибка на {misstake} позиции");
            }
        }
        private int ChangeMisstake(string zacodirText)
        {
            int misstakeIndex = 0;
            bool flag = false;
            int pow = 0;
            for (int i = 1; i <= zacodirText.Length; i++)
            {
                if (i == Convert.ToInt32(Math.Pow(2, pow)))
                {
                    int sum = 0;
                    for (int j = i; j <= zacodirText.Length; j += (i * 2))
                    {
                        for (int k = j; k < j + i; k++)
                        {
                            if (j == i && k == j) continue;
                            if (k > zacodirText.Length) break;
                            sum += int.Parse(zacodirText[k - 1].ToString());
                        }
                    }
                    if (sum % 2 != int.Parse(zacodirText[i - 1].ToString()))
                    {
                        flag = true;
                        misstakeIndex += i;
                    }
                    pow++;
                }
            }
            if (flag) return misstakeIndex;
            else return -1;
        }
    }
}
