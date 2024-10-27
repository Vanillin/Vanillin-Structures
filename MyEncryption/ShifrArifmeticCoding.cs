using System;
using System.Collections.Generic;

namespace MyEncryption
{
    public class ShifrArifmeticCoding : IShift
    {
        private void ChandeP(char c, ref int Count, ref List<(char sym, int coun)> P)
        {
            Count++;
            for (int i = 0; i < P.Count; i++)
            {
                if (P[i].sym == c)
                {
                    P[i] = (P[i].sym, P[i].coun + 1);
                    return;
                }
            }
            throw new Exception("Symbol is not contained in P");
        }
        private void CreateP(string Text, ref int Count, ref List<(char sym, int coun)> P, out Dictionary<string, string> Diction)
        {
            Diction = new Dictionary<string, string>();
            foreach (char c in Text.ToCharArray())
            {
                if (!Diction.ContainsKey(c.ToString()))
                {
                    Diction.Add(c.ToString(), " ");
                    P.Add((c, 1));
                    Count++;
                }
            }
            Diction.Add("eof", Text.Length.ToString());
        }

        public void Codirov(string Text, out string zacodirText, out Dictionary<string, string> Diction, out int WeightZacodirText)
        {
            List<(char sym, int coun)> P = new List<(char sym, int prob)>();
            int Count = 0;

            CreateP(Text, ref Count, ref P, out Diction);

            double left = 0;
            double right = 1;
            double length;

            foreach (char c in Text.ToCharArray())
            {
                length = right - left;
                double newleft = left;
                bool IsZacodir = false;

                for (int i = 0; i < P.Count; i++)
                {
                    double shift;
                    if (P[i].sym != c)
                    {
                        shift = P[i].coun / (double)Count;
                        newleft += shift * length;
                    }
                    else
                    {
                        if (i + 1 != P.Count)
                        {
                            shift = P[i + 1].coun / (double)Count;
                            right = newleft + shift * length;
                        }
                        IsZacodir = true;
                        break;
                    }

                }
                if (IsZacodir)
                {
                    left = newleft;
                }

                ChandeP(c, ref Count, ref P);
            }

            if (right != 1 && left != 0)
            {
                string numberleft = left.ToString();
                string numberright = right.ToString();
                zacodirText = "";
                for (int i = 2; i < Math.Min(numberleft.Length, numberright.Length); i++)
                {
                    if (numberleft[i] == numberright[i])
                        zacodirText += numberleft[i];
                    else
                    {
                        zacodirText += numberright[i];
                        break;
                    }
                }
            }
            else
            {
                if (left == 0)
                    zacodirText = "0";
                else
                    zacodirText = "99999";
            }
            WeightZacodirText = zacodirText.Length * 4;
        }

        public void Decodirov(string zacodirText, Dictionary<string, string> Diction, out string Text)
        {
            List<(char sym, int coun)> P = new List<(char sym, int prob)>();
            int Count = 0;
            int CountCycles = 0;
            double zacodir = double.Parse("0," + zacodirText);
            Text = "";

            foreach (var v in Diction)
            {
                if (v.Key != "eof")
                {
                    P.Add((v.Key[0], 1));
                    Count++;
                }
                else
                {
                    CountCycles = int.Parse(v.Value);
                    break;
                }
            }

            double left = 0;
            double right = 1;
            double length;

            for (int k = 0; k < CountCycles; k++)
            {
                length = right - left;
                double newleft = left;
                double newrigth = left;

                for (int i = 0; i < P.Count; i++)
                {
                    double shift = P[i].coun / (double)Count;
                    newrigth += shift * length;
                    if (zacodir <= newrigth)
                    {
                        left = newleft;
                        right = newrigth;
                        Text += P[i].sym;

                        ChandeP(P[i].sym, ref Count, ref P);
                        break;
                    }
                    newleft = newrigth;
                }
            }
        }
    }
}
