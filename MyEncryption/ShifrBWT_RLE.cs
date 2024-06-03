using System;
using System.Collections.Generic;

namespace MyEncryption
{
    public class ShifrBWT_RLE : IShift
    {
        private string ToRLE(string str, out int max)
        {
            string retur = "";
            int count = 1;
            max = 1;
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == str[i - 1])
                {
                    count++;
                }
                else
                {
                    retur += str[i - 1];
                    retur += count.ToString();
                    max = Math.Max(max, count);
                    count = 1;
                }
            }
            retur += str[str.Length - 1];
            retur += count.ToString();
            max = Math.Max(max, count);
            return retur;
        }
        private string AboutRLE(string str)
        {
            string retur = "";
            for (int i = 0; i < str.Length; i += 2)
            {
                char c = str[i];
                int count = int.Parse(str[i + 1].ToString());
                for (int j = 0; j < count; j++)
                {
                    retur += c;
                }
            }
            return retur;
        }
        private int OpredelitWeight(int lengthBit)
        {
            int stepen = 1;
            while (Math.Pow(2, stepen) < lengthBit)
            {
                stepen++;
            }
            return stepen;
        }
        public void Codirov(string Text, out string zacodirText, out Dictionary<string, string> Diction, out int WeightZacodirText)
        {
            Diction = new Dictionary<string, string>();
            List<string> strings = new List<string>(Text.Length);
            int shift = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                string str = "";
                for (int k = 0; k < Text.Length; k++)
                    str += Text[((k - shift) + Text.Length) % Text.Length];
                strings.Add(str);
                shift++;
            }
            strings.Sort();
            int index = strings.FindIndex(x => x == Text);

            zacodirText = "";
            for (int i = 0; i < strings.Count; i++)
            {
                zacodirText += strings[i][Text.Length - 1];
            }

            zacodirText = ToRLE(zacodirText, out int max);
            zacodirText += $",{index}";
            WeightZacodirText = zacodirText.Length / 2 * (OpredelitWeight(max) + 8) + OpredelitWeight(Text.Length);
        }

        public void Decodirov(string zacodirText, Dictionary<string, string> Diction, out string Text)
        {
            string[] zacodStr = zacodirText.Split(',');
            zacodirText = AboutRLE(zacodStr[0]);
            List<char> chars = new List<char>(zacodirText.Length);
            List<string> strings = new List<string>(zacodirText.Length);

            for (int i = 0; i < zacodirText.Length; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < zacodirText.Length; j++)
                    {
                        chars.Add(zacodirText[j]);
                        strings.Add(zacodirText[j].ToString());
                    }
                }
                else
                {
                    for (int j = 0; j < zacodirText.Length; j++)
                    {
                        strings[j] = chars[j] + strings[j];
                    }
                }
                strings.Sort();
            }

            Text = strings[int.Parse(zacodStr[1])];
        }
    }
}
