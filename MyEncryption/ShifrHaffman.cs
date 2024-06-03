using System.Collections.Generic;
using System.Linq;

namespace MyEncryption
{
    public class ShifrHaffman : IShift
    {
        public void Codirov(string Text, out string zacodirText, out Dictionary<string, string> Diction, out int WeightZacodirText)
        {
            // Вычисление частоты каждого символа в тексте
            Dictionary<string, int> frequencies = CalculateFrequencies(Text);

            // Составление списка символов алфавита в порядке убывания их вероятностей
            List<Node> nodes = frequencies.Select(pair => new Node(pair.Key, pair.Value)).OrderByDescending(n => n.Frequency).ToList();

            // Построение дерева Хаффмана
            Node root = BuildHuffmanTree(nodes);

            // Генерация кодов для каждого символа
            Dictionary<string, string> codes = GenerateHuffmanCodes(root);

            // Кодирование текста
            zacodirText = EncodeText(Text, codes);

            // Возвращаем созданный словарь кодов
            Diction = codes;

            // Вычисляем общий вес закодированного текста
            WeightZacodirText = zacodirText.Length;
        }

        public void Decodirov(string zacodirText, Dictionary<string, string> Diction, out string Text)
        {
            // Декодируем текст, используя переданный словарь кодов
            Text = DecodeText(zacodirText, Diction);
        }

        private Dictionary<string, int> CalculateFrequencies(string text)
        {
            Dictionary<string, int> frequencies = new Dictionary<string, int>();
            foreach (char c in text)
            {
                if (frequencies.ContainsKey(c.ToString()))
                {
                    frequencies[c.ToString()]++;
                }
                else
                {
                    frequencies[c.ToString()] = 1;
                }
            }
            return frequencies;
        }

        private Node BuildHuffmanTree(List<Node> nodes)
        {
            while (nodes.Count > 1)
            {
                // Выбираем два узла с наименьшей частотой
                Node left = nodes[nodes.Count - 1];
                Node right = nodes[nodes.Count - 2];

                // Создаем новый узел, который объединяет два выбранных узла
                Node parent = new Node("\0", left.Frequency + right.Frequency);
                parent.Left = left;
                parent.Right = right;

                // Удаляем использованные узлы и добавляем новый узел в список
                nodes.RemoveAt(nodes.Count - 1);
                nodes.RemoveAt(nodes.Count - 1);
                nodes.Add(parent);

                // Сортируем узлы по убыванию частоты
                nodes = nodes.OrderByDescending(n => n.Frequency).ToList();
            }

            // Возвращаем корневой узел дерева Хаффмана
            return nodes.Single();
        }

        private Dictionary<string, string> GenerateHuffmanCodes(Node node)
        {
            Dictionary<string, string> codes = new Dictionary<string, string>();
            GenerateHuffmanCodes(node, "", codes);
            return codes;
        }

        private void GenerateHuffmanCodes(Node node, string code, Dictionary<string, string> codes)
        {
            if (node.IsLeaf())
            {
                codes[node.Character.ToString()] = code;
                return;
            }

            if (node.Left != null)
            {
                GenerateHuffmanCodes(node.Left, code + "0", codes);
            }

            if (node.Right != null)
            {
                GenerateHuffmanCodes(node.Right, code + "1", codes);
            }
        }

        private string EncodeText(string text, Dictionary<string, string> codes)
        {
            string encodedText = "";
            foreach (char c in text)
            {
                encodedText += codes[c.ToString()];
            }
            return encodedText;
        }

        private string DecodeText(string encodedText, Dictionary<string, string> codes)
        {
            string decodedText = "";
            string currentCode = "";
            foreach (char bit in encodedText)
            {
                currentCode += bit;
                if (codes.ContainsValue(currentCode))
                {
                    decodedText += codes.FirstOrDefault(x => x.Value == currentCode).Key;
                    currentCode = "";
                }
            }
            return decodedText;
        }


        class Node
        {
            public string Character { get; }
            public int Frequency { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(string character, int frequency)
            {
                Character = character;
                Frequency = frequency;
                Left = null;
                Right = null;
            }

            public bool IsLeaf()
            {
                return Left == null && Right == null;
            }
        }
    }
}