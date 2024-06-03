using System;
using System.Collections.Generic;
using System.Text;

namespace MyEncryption
{
    public class ShifrLZ77 : IShift
    {
        public void Codirov(string Text, out string zacodirText, out Dictionary<string, string> Diction, out int WeightZacodirText)
        {
            double maxLength = 0;
            double maxShift = 0;
            int length = 0;
            int shiftMark,
                lengthMark;
            string symbolMark;
            zacodirText = "";
            Diction = new Dictionary<string, string>();
            int countMark = 0;
            StringBuilder SearchBuffer = new StringBuilder();
            for (int PreemptiveBuffer = 0; PreemptiveBuffer < Text.Length;) //Упреждающий буфер
            {
                shiftMark = 0;
                lengthMark = 0;
                char symbol = Text[PreemptiveBuffer];
                symbolMark = symbol.ToString();
                int shift = 0;
                for (int mark = SearchBuffer.Length - 1; mark >= 0; mark--) // Буфер поиска
                {
                    shift++;
                    if (symbol == SearchBuffer[mark])
                    {
                        length = 0;
                        for (int search = mark; search < SearchBuffer.Length; search++) //Проверка на количество совпадений   
                        {
                            if (SearchBuffer[search] == Text[PreemptiveBuffer + (search - mark)])
                            {
                                length++;
                                if (PreemptiveBuffer + length == Text.Length)
                                {
                                    if (length > maxLength) maxLength = length;
                                    if (shift > maxShift) maxShift = shift;
                                    symbolMark = "eof";
                                    lengthMark = length;
                                    shiftMark = shift;
                                    break;
                                }
                            }
                            else break;
                        }
                        if (length >= lengthMark)
                        {
                            if (length > maxLength) maxLength = length;
                            if (shift > maxShift) maxShift = shift;
                            lengthMark = length;
                            shiftMark = shift;
                            if (PreemptiveBuffer + lengthMark == Text.Length)
                            {
                                symbolMark = "eof";
                            }
                            else symbolMark = Text[PreemptiveBuffer + lengthMark].ToString();
                        }
                    }
                }
                zacodirText += "(" + shiftMark.ToString() + "," + lengthMark.ToString() + "," + symbolMark.ToString() + ") ";
                countMark++;
                if (PreemptiveBuffer + lengthMark == Text.Length) break;
                SearchBuffer.Append(Text, PreemptiveBuffer, lengthMark + 1);
                PreemptiveBuffer += lengthMark + 1;
            }
            WeightZacodirText = countMark *
                (8 + Convert.ToInt32(Math.Ceiling(Math.Log(maxShift + 1, 2))) + Convert.ToInt32(Math.Ceiling(Math.Log(maxLength + 1, 2))));
        }

        public void Decodirov(string zacodirText, Dictionary<string, string> Diction, out string Text)
        {
            StringBuilder decoding = new StringBuilder();
            string[] Marks = zacodirText.Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Marks.Length; i += 3)
            {
                decoding.Append(decoding.ToString(), decoding.Length - int.Parse(Marks[i]), int.Parse(Marks[i + 1]));
                if (Marks[i + 2] != "eof") decoding.Append(Marks[i + 2]);
            }
            Text = decoding.ToString();
        }
    }
}
