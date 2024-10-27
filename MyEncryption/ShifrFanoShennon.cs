using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEncryption
{
    public class ShifrFanoShennon : IShift
    {
        static Dictionary<string, int> CalculateCharacterFrequencies(string text)
        {
            Dictionary<string, int> charFrequencies = new Dictionary<string, int>();
            foreach (char c in text)
            {
                if (charFrequencies.ContainsKey(c.ToString()))
                {
                    charFrequencies[c.ToString()]++;
                }
                else
                {
                    charFrequencies[c.ToString()] = 1;
                }
            }
            return charFrequencies;
        }

        static List<Node> CreateNodes(Dictionary<string, int> charFrequencies)
        {
            List<Node> nodes = new List<Node>();
            foreach (var pair in charFrequencies)
            {
                nodes.Add(new Node(pair.Key, pair.Value));
            }
            return nodes;
        }

        public void Codirov(string Text, out string zacodirText, out Dictionary<string, string> Diction, out int WeightZacodirText)
        {
            Dictionary<string, int> charFrequencies = CalculateCharacterFrequencies(Text);
            List<Node> nodes = CreateNodes(charFrequencies);
            nodes.Sort((a, b) => b.Frequency.CompareTo(a.Frequency));
            string code = "";
            Stack<Tuple<List<Node>, string>> stack = new Stack<Tuple<List<Node>, string>>();
            stack.Push(new Tuple<List<Node>, string>(nodes, code));

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                var currentNodes = current.Item1;
                var currentCode = current.Item2;

                if (currentNodes.Count == 1)
                {
                    currentNodes[0].Code = currentCode;
                    continue;
                }

                int totalFrequency = currentNodes.Sum(n => n.Frequency);
                int halfFrequency = totalFrequency / 2;
                int cumulativeFrequency = 0;
                int index = 0;

                for (int i = 0; i < currentNodes.Count; i++)
                {
                    cumulativeFrequency += currentNodes[i].Frequency;
                    if (cumulativeFrequency > halfFrequency)
                    {
                        index = i;
                        break;
                    }
                }

                if (index == 0)
                {
                    index = 1;
                }

                for (int i = 0; i < index; i++)
                {
                    currentNodes[i].Code += currentCode + "0";
                }

                for (int i = index; i < currentNodes.Count; i++)
                {
                    currentNodes[i].Code += currentCode + "1";
                }

                stack.Push(new Tuple<List<Node>, string>(currentNodes.Take(index).ToList(), currentCode + "0"));
                stack.Push(new Tuple<List<Node>, string>(currentNodes.Skip(index).ToList(), currentCode + "1"));

            }
            Diction = new Dictionary<string, string>();
            foreach (var node in nodes)
            {
                Diction.Add(node.Character, node.Code);
            }
            StringBuilder encodedText = new StringBuilder();

            foreach (char c in Text)
            {
                if (Diction.ContainsKey(c.ToString()))
                {
                    encodedText.Append(Diction[c.ToString()]);
                }
            }
            zacodirText = encodedText.ToString();
            WeightZacodirText = zacodirText.Length * 2;

        }

        public void Decodirov(string zacodirText, Dictionary<string, string> Diction, out string Text)
        {
            Dictionary<string, string> Dictione = Diction.ToDictionary(pair => pair.Value, pair => pair.Key);
            StringBuilder decodedText = new StringBuilder();
            int index = 0;

            while (index < zacodirText.Length)
            {
                foreach (var code in Dictione.Keys)
                {
                    if (zacodirText.Substring(index).StartsWith(code))
                    {
                        decodedText.Append(Dictione[code]);
                        index += code.Length;
                        break;
                    }
                }
            }
            Text = decodedText.ToString();

        }
    }
    class Node
    {
        public string Character { get; }
        public int Frequency { get; }
        public string Code { get; set; }

        public Node(string character, int frequency)
        {
            Character = character;
            Frequency = frequency;
            Code = "";
        }
    }
}
